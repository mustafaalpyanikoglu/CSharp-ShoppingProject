using Application.Features.Auths.Rules;
using Business.Features.Auths.Constants;
using Business.Features.Auths.Dtos;
using Business.Features.Users.Constants;
using Business.Services.UserService;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public AuthManager(IUserService userService, IUserDal userDal, ITokenHelper tokenHelper, 
            AuthBusinessRules authBusinessRules, IUserOperationClaimDal userOperationClaimDal)
        {
            _userService = userService;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
            _authBusinessRules = authBusinessRules;
            _userOperationClaimDal = userOperationClaimDal;
        }

        public async Task<IResult> ChangePassword(UserForChangePasswordDto userForChangePasswordDto)
        {
            byte[] passwordHash, passwordSalt;

            IDataResult<User>? userResult = await _userService.GetByUserName(userForChangePasswordDto.UserName);

            HashingHelper.CreatePasswordHash(userForChangePasswordDto.Password, out passwordHash, out passwordSalt);
            userResult.Data.PasswordHash = passwordHash;
            userResult.Data.PasswordSalt = passwordSalt;
            await _userDal.UpdateAsync(userResult.Data);

            return new SuccessResult(AuthMessages.ChangePassword);
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IList<OperationClaim> operationClaims = await _userOperationClaimDal
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
            IDataResult<User>? user = await _userService.GetByUserName(userForLoginDto.UserName);
            await _authBusinessRules.UserShouldBeExists(user.Data);
            await _authBusinessRules.UserPasswordShouldBeMatch(user.Data.Id, userForLoginDto.Password);

            if (user.Data == null)
            {
                return new ErrorDataResult<User>(UserMessages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password ?? "", user.Data.PasswordHash, user.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(user.Data, UserMessages.PasswordError);
            }
            return new SuccessDataResult<User>(user.Data, UserMessages.SuccesfulLogin);
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            await _authBusinessRules.UserNameShouldBeNotExists(userForRegisterDto.UserName);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            User user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                UserName = userForRegisterDto.UserName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                RegistrationDate = Convert.ToDateTime(DateTime.Now.ToString("F"))
            };

            await _userService.Add(user);
            return new SuccessDataResult<User>(user, UserMessages.UserRegistered);
        }

        public async Task<IResult> UserExists(string userName)
        {
            IDataResult<User> result = await _userService.GetByUserName(userName);
            if (result.Success)
            {
                return new SuccessResult(UserMessages.UserAlreadyExists);
            }
            return new ErrorResult(UserMessages.MailAvailable);
        }
    }
}
