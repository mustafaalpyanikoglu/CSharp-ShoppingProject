using Business.Features.Purses.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace Business.Features.Purses.Rules
{
    public class PurseBusinessRules : BaseBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurseBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task PurseIdShouldExistWhenSelected(int? id)
        {
            Purse? result = await _unitOfWork.PurseDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(PurseMessages.PurseNotFound);
        }

        public Task MoneyToBeAddedMustBeMoreThanZero(float money)
        {
            if (money <= 0) throw new BusinessException(PurseMessages.InvalidAmountOfMoney);
            return Task.CompletedTask;
        }
        public Task IsTheMoneyEnough(float totalPrice, float moneyInAccount)
        {
            if (totalPrice > moneyInAccount ) throw new BusinessException(PurseMessages.InvalidAmountOfMoney);
            return Task.CompletedTask;
        }

        public Task TheMoneyToBeSpentCannotBeMoreThanTheAmountInTheWallet(float money, float spendMoney)
        {
            if (money < spendMoney) throw new BusinessException(PurseMessages.InsufficientMoney);
            return Task.CompletedTask;
        }

        public async Task OperationMustBeAvailable()
        {
            List<Purse>? results = _unitOfWork.PurseDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(PurseMessages.PurseNotFound);
        }
        public IDataResult<List<Purse>> MustBeARegisteredPurse()
        {
            List<Purse>? result = _unitOfWork.PurseDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(PurseMessages.PurseNotFound);
            return new SuccessDataResult<List<Purse>>(result, PurseMessages.PurseAvaliable);
        }

        public async Task<IDataResult<Purse>> ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(int id)
        {
            Purse? result = await _unitOfWork.PurseDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(PurseMessages.PurseNotFound);
            return new SuccessDataResult<Purse>(result, PurseMessages.PurseAvaliable);
        }
    }
}
