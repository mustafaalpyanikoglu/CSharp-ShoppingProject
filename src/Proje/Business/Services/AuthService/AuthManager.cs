using Application.Features.Auths.Rules;
using Business.Features.Auths.Dtos;
using Business.Services.UserService;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Core.Security.Hashing;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Core.Security.Jwt;
using DataAccess.Concrete.EfUnitOfWork;
using static Business.Features.Auths.Constants.AuthMessages;
using static Business.Features.Users.Constants.UserMessages;

namespace Business.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUnitOfWork _unitOfWork;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _authBusinessRules = authBusinessRules;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> ChangePassword(UserForChangePasswordDto userForChangePasswordDto)
        {
            byte[] passwordHash, passwordSalt;

            IDataResult<User>? userResult = await _userService.GetUserByEmail(userForChangePasswordDto.Email);

            HashingHelper.CreatePasswordHash(userForChangePasswordDto.Password, out passwordHash, out passwordSalt);
            userResult.Data.PasswordHash = passwordHash;
            userResult.Data.PasswordSalt = passwordSalt;
            await _unitOfWork.UserDal.UpdateAsync(userResult.Data);

            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(PasswordChangedSuccessfully);
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IList<OperationClaim> operationClaims = await _unitOfWork.UserOperationClaimDal
                    .Query()
                    .AsNoTracking()
                    .Where(p => p.UserId == user.Id)
                    .Select(p => new OperationClaim
                    {
                        Id = p.OperationClaimId,
                        Name = p.OperationClaim.Name
                    })
                    .ToListAsync();

            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
            return accessToken;
        }

        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            IDataResult<User>? user = await _userService.GetUserByEmail(userForLoginDto.Email);
            await _authBusinessRules.UserShouldBeExists(user.Data);
            await _authBusinessRules.UserPasswordShouldBeMatch(user.Data.Id, userForLoginDto.Password);

            if (user.Data == null)
            {
                return new ErrorDataResult<User>(UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password ?? "", user.Data.PasswordHash, user.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(user.Data, PasswordError);
            }
            return new SuccessDataResult<User>(user.Data, SuccesfulLogin);
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(userForRegisterDto.Email);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            User user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Address = userForRegisterDto.Address,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                RegistrationDate = Convert.ToDateTime(DateTime.Now.ToString("F"))
            };

            await _userService.Add(user);

            User? addRolToUser = await _unitOfWork.UserDal.GetAsync(u => u.Email == user.Email);
            await _unitOfWork.UserOperationClaimDal.AddAsync(new UserOperationClaim
            {
                UserId = addRolToUser.Id,
                OperationClaimId = 2
            });

            await _unitOfWork.PurseDal.AddAsync(new Purse
            {
                UserId = addRolToUser.Id,
                Money = 10
            });

            await _unitOfWork.UserCartDal.AddAsync(new UserCart
            {
                UserId = addRolToUser.Id
            });

            await _unitOfWork.SaveChangesAsync();

            return new SuccessDataResult<User>(user, UserRegistered);
        }

        public async Task<IResult> UserExists(string userName)
        {
            IDataResult<User> result = await _userService.GetUserByEmail(userName);
            if (result.Success)
            {
                return new SuccessResult(UserAlreadyExists);
            }
            return new ErrorResult(UserEmailNotAvaliable);
        }
    }
}
