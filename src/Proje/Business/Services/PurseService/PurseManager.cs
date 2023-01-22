using Business.Features.Purses.Rules;
using DataAccess.Abstract;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;

namespace Business.Services.PurseService
{
    public class PurseManager : IPurseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PurseBusinessRules _purseBusinessRules;

        public PurseManager(IUnitOfWork unitOfWork, PurseBusinessRules purseBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _purseBusinessRules = purseBusinessRules;
        }

        public async Task<Purse> AddMoney(Purse purse, float addMoney)
        {
            await _purseBusinessRules.MoneyToBeAddedMustBeMoreThanZero(addMoney);

            purse.Money += addMoney;
            Purse updatePurse = await _unitOfWork.PurseDal.UpdateAsync(purse);

            await _unitOfWork.SaveChangesAsync();

            return updatePurse;
        }

        public async Task<Purse> SpendMoney(Purse purse, float spendMoney)
        {
            await _purseBusinessRules.TheMoneyToBeSpentCannotBeMoreThanTheAmountInTheWallet(purse.Money, spendMoney);

            purse.Money -= spendMoney;
            Purse updatePurse = await _unitOfWork.PurseDal.UpdateAsync(purse);

            await _unitOfWork.SaveChangesAsync();

            return updatePurse;
        }
    }
}
