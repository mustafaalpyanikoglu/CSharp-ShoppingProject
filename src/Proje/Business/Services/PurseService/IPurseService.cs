using Entities.Concrete;

namespace Business.Services.PurseService
{
    public interface IPurseService
    {
        public Task<Purse> AddMoney(Purse purse,float money);

        public Task<Purse> SpendMoney(Purse purse, float spendMoney);
    }
}
