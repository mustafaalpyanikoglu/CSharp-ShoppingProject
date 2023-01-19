using Business.Features.OperationClaims.Constants;
using Business.Features.UserOperationClaims.Constants;
using Business.Features.Users.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules : BaseBusinessRules
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IUserDal _userDal;
        private readonly IOperationClaimDal _operationClaimDal;

        public UserOperationClaimBusinessRules(IUserOperationClaimDal userOperationClaimDal, IUserDal userDal, IOperationClaimDal operationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _userDal = userDal;
            _operationClaimDal = operationClaimDal;
        }

        public async Task UserOperationClaimMustBeAvailable()
        {
            List<UserOperationClaim>? results = _userOperationClaimDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(UserOperationClaimMessages.UserOperationClaimNotFound);
        }

        public async Task UserOperationClaimIdMustBeAvailable(int userOperationClaimId)
        {
            UserOperationClaim? result = await _userOperationClaimDal.GetAsync(t => t.Id == userOperationClaimId);
            if (result == null) throw new BusinessException(UserOperationClaimMessages.UserOperationClaimNotFound);
        }
        public async Task UserIdMustBeAvailable(int userId)
        {
            User? result = await _userDal.GetAsync(t => t.Id == userId);
            if (result == null) throw new BusinessException(UserMessages.UserNotFound);
        }
        public async Task OperationClaimIdMustBeAvailable(int operationClaimId)
        {
            OperationClaim? result = await _operationClaimDal.GetAsync(t => t.Id == operationClaimId);
            if (result == null) throw new BusinessException(OperationClaimMessages.OperationClaimNotFound);
        }


        public IDataResult<List<UserOperationClaim>> MustBeARegisteredUserOperationClaim()
        {
            List<UserOperationClaim>? result = _userOperationClaimDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(UserOperationClaimMessages.UserOperationClaimNotFound);
            return new SuccessDataResult<List<UserOperationClaim>>(result, UserOperationClaimMessages.UserOperationClaimFound);
        }

        public async Task<IDataResult<UserOperationClaim>> UserOperationClaimIdShouldExistWhenSelected(int id)
        {
            UserOperationClaim? result = await _userOperationClaimDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(UserOperationClaimMessages.UserOperationClaimNotFound);
            return new SuccessDataResult<UserOperationClaim>(result, UserOperationClaimMessages.UserOperationClaimFound);
        }
    }
}
