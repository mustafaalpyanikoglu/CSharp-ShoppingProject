using Business.Features.Users.Constants;
using Business.Features.Users.Rules;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;

namespace Business.Services.UserService
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserBusinessRules _userBusinessRules;

        public UserManager(IUnitOfWork unitOfWork, UserBusinessRules userBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<IResult> Add(User user)
        {
            await _unitOfWork.UserDal.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(UserMessages.UserAdded);
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        {
            IDataResult<User> userGetClaims = await _userBusinessRules.UserIdShouldExistWhenSelected(user.Id);
            return new SuccessDataResult<List<OperationClaim>>(_unitOfWork.UserDal.GetClaims(userGetClaims.Data), UserMessages.UserRolesListed);
        }

        public async Task<IDataResult<User>> GetUserByEmail(string email)
        {
            IDataResult<User> userResult = await _userBusinessRules.UserEmailMustBePresent(email);
            return new SuccessDataResult<User>(userResult.Data, userResult.Message);
        }

    }
}
