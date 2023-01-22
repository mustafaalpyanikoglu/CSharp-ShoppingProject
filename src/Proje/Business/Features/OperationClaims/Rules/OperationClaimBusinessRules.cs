using Business.Features.OperationClaims.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;

namespace Business.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules:BaseBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public OperationClaimBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OperationClaimIdShouldExistWhenSelected(int? id)
        {
            OperationClaim? result = await _unitOfWork.OperationClaimDal.GetAsync(b => b.Id == id);
            if (result ==null) throw new BusinessException(OperationClaimMessages.OperationClaimNotFound);
        }
        public async Task OperationClaimNameShouldBeNotExists(string name)
        {
            OperationClaim? user = await _unitOfWork.OperationClaimDal.GetAsync(u => u.Name.ToLower() == name.ToLower());
            if (user != null) throw new BusinessException(OperationClaimMessages.OperationClaimNameAlreadyExists);
        }

        public async Task OperationMustBeAvailable()
        {
            List<OperationClaim>? results = _unitOfWork.OperationClaimDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(OperationClaimMessages.OperationClaimNotFound);
        }
        public IDataResult<List<OperationClaim>> MustBeARegisteredOperationClaim()
        {
            List<OperationClaim>? result = _unitOfWork.OperationClaimDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(OperationClaimMessages.OperationClaimNotFound);
            return new SuccessDataResult<List<OperationClaim>>(result, OperationClaimMessages.OperationClaimAvaliable);
        }

        public async Task<IDataResult<OperationClaim>> ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(int id)
        {
            OperationClaim? result = await _unitOfWork.OperationClaimDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(OperationClaimMessages.OperationClaimNotFound);
            return new SuccessDataResult<OperationClaim>(result, OperationClaimMessages.OperationClaimAvaliable);
        }
    }
}
