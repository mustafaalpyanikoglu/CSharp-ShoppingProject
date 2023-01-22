using Business.Features.Users.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Entities.Concrete;
using DataAccess.Concrete.Contexts;
using static Business.Features.OperationClaims.Constants.OperationClaimMessages;
using static Business.Features.UserOperationClaims.Constants.UserOperationClaimMessages;

namespace Business.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules : BaseBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserOperationClaimBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UserOperationClaimMustBeAvailable()
        {
            List<UserOperationClaim>? results = _unitOfWork.UserOperationClaimDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(UserOperationClaimNotFound);
        }

        public async Task UserOperationClaimIdMustBeAvailable(int userOperationClaimId)
        {
            UserOperationClaim? result = await _unitOfWork.UserOperationClaimDal.GetAsync(t => t.Id == userOperationClaimId);
            if (result == null) throw new BusinessException(UserOperationClaimNotFound);
        }
        public async Task UserIdMustBeAvailable(int userId)
        {
            User? result = await _unitOfWork.UserDal.GetAsync(t => t.Id == userId);
            if (result == null) throw new BusinessException(UserMessages.UserNotFound);
        }
        public async Task OperationClaimIdMustBeAvailable(int operationClaimId)
        {
            OperationClaim? result = await _unitOfWork.OperationClaimDal.GetAsync(t => t.Id == operationClaimId);
            if (result == null) throw new BusinessException(OperationClaimNotFound);
        }


        public IDataResult<List<UserOperationClaim>> MustBeARegisteredUserOperationClaim()
        {
            List<UserOperationClaim>? result = _unitOfWork.UserOperationClaimDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(UserOperationClaimNotFound);
            return new SuccessDataResult<List<UserOperationClaim>>(result, UserOperationClaimFound);
        }

        public async Task<IDataResult<UserOperationClaim>> UserOperationClaimIdShouldExistWhenSelected(int id)
        {
            UserOperationClaim? result = await _unitOfWork.UserOperationClaimDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(UserOperationClaimNotFound);
            return new SuccessDataResult<UserOperationClaim>(result, UserOperationClaimFound);
        }
    }
}
