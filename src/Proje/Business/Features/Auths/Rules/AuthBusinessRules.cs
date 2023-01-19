using Business.Features.Auths.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Application.Features.Auths.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserDal _userdal;

    public AuthBusinessRules(IUserDal userdal)
    {
        _userdal = userdal;
    }
    public Task UserShouldBeExists(User? user)
    {
        if (user == null) throw new BusinessException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }

    public async Task UserNameShouldBeNotExists(string userName)
    {
        User? user = await _userdal.GetAsync(u => u.UserName == userName);
        if (user != null) throw new BusinessException(AuthMessages.UserNameAlreadyExists);
    }

    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        User? user = await _userdal.GetAsync(u => u.Id == id);
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException(AuthMessages.PasswordDontMatch);
    }
}