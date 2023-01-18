using Business.Features.Auths.Constants;
using Business.Features.Users.Constants;
using Core.Business.Rules;
using Core.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Features.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserDal _userDal;

    public UserBusinessRules(IUserDal userDal)
    {
        _userDal = userDal;
    }
    public async Task UserIdMustBeAvailable(int id)
    {
        User? result = await _userDal.GetAsync(t => t.Id == id);
        if (result == null) throw new BusinessException(UserMessages.UserNotFound);
    }

    public async Task UserNameMustNotExist(string userName)
    {
        User? result = await _userDal.GetAsync(t => t.UserName == userName);
        if (result != null) throw new BusinessException(UserMessages.UserNameAvaliable);
    }

    public async Task UserMustBeAvailable()
    {
        List<User>? results = _userDal.GetAll();
        if (results.Count <= 0) throw new BusinessException(UserMessages.UserNotFound);
    }
    public async Task<IDataResult<User>> UserIdShouldExistWhenSelected(int id)
    {
        User? result = await _userDal.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(UserMessages.UserNotFound);
        return new SuccessDataResult<User>(result);
    }

    public async Task<IDataResult<User>> UserNameMustBePresent(string userName)
    {
        User? result = await _userDal.GetAsync(b => b.UserName.ToLower() == userName.ToLower());
        if (result == null) throw new BusinessException(UserMessages.UserNameNotAvaliable);
        return new SuccessDataResult<User>(result, UserMessages.UserNameAvaliable);
    }

    public IDataResult<List<User>> MustBeARegisteredUser()
    {
        List<User>? result = _userDal.GetAll();
        if (result == null) throw new BusinessException(UserMessages.UserNotFound);
        return new SuccessDataResult<List<User>>(result);
    }

    public Task UserShouldBeExist(User? user)
    {
        if (user is null) throw new BusinessException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }

    public Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException(AuthMessages.PasswordDontMatch);
        return Task.CompletedTask;
    }
}