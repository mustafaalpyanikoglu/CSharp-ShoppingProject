using Business.Features.Purses.Rules;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Services.PurseService
{
    public class PurseManager : IPurseService
    {
        private readonly IPurseDal _purseDal;
        private readonly PurseBusinessRules _purseBusinessRules;

        public PurseManager(IPurseDal purseDal, PurseBusinessRules purseBusinessRules)
        {
            _purseDal = purseDal;
            _purseBusinessRules = purseBusinessRules;
        }

        public async Task<Purse> AddMoney(Purse purse, float addMoney)
        {
            await _purseBusinessRules.MoneyToBeAddedMustBeMoreThanZero(addMoney);

            purse.Money += addMoney;
            Purse updatePurse = await _purseDal.UpdateAsync(purse);

            return updatePurse;
        }

        public async Task<Purse> SpendMoney(Purse purse, float spendMoney)
        {
            await _purseBusinessRules.TheMoneyToBeSpentCannotBeMoreThanTheAmountInTheWallet(purse.Money, spendMoney);

            purse.Money -= spendMoney;
            Purse updatePurse = await _purseDal.UpdateAsync(purse);

            return updatePurse;
        }
    }
}
