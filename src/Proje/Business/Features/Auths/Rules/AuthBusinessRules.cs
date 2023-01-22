using Business.Features.Auths.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace Application.Features.Auths.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUnitOfWork _unitOfWork;
    public AuthBusinessRules(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task UserShouldBeExists(User? user)
    {
        if (user == null) throw new BusinessException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }
    public Task PasswordsEnteredMustBeTheSame(string newPassword, string repeatPassword)
    {
        if (newPassword != repeatPassword) throw new BusinessException(AuthMessages.PasswordDoNotMatch);
        return Task.CompletedTask;
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        User? user = await _unitOfWork.UserDal.GetAsync(u => u.Email == email);
        if (user != null) throw new BusinessException(AuthMessages.UserEmailAlreadyExists);
    }

    public async Task UserEmailMustBeAvailable(string email)
    {
        User? user = await _unitOfWork.UserDal.GetAsync(u => u.Email == email);
        if (user == null) throw new BusinessException(AuthMessages.UserDontExists);
    }

    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        User? user = await _unitOfWork.UserDal.GetAsync(u => u.Id == id);
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException(AuthMessages.PasswordDontMatch);
    }
}