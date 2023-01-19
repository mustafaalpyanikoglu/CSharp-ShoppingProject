using Business.Features.Users.Constants;
using Business.Features.Users.Rules;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Services.UserService
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly UserBusinessRules _userBusinessRules;

        public UserManager(IUserDal userDal, UserBusinessRules userBusinessRules)
        {
            _userDal = userDal;
            _userBusinessRules = userBusinessRules;
        }

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IResult> Add(User user)
        {
            await _userDal.AddAsync(user);
            return new SuccessResult(UserMessages.UserAdded);
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        {
            IDataResult<User> userGetClaims = await _userBusinessRules.UserIdShouldExistWhenSelected(user.Id);
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(userGetClaims.Data), UserMessages.UserRolesListed);
        }

        public async Task<IDataResult<User>> GetUserByEmail(string email)
        {
            IDataResult<User> userResult = await _userBusinessRules.UserEmailMustBePresent(email);
            return new SuccessDataResult<User>(userResult.Data, userResult.Message);
        }

    }
}
