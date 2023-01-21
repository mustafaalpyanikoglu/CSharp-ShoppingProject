using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Constants;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPurseDal : EfRepositoryBase<Purse, BaseDbContext>, IPurseDal
    {
        public EfPurseDal(BaseDbContext context) : base(context)
        {
        }

        public Task<Purse> GetByUserCartId(int userCartId)
        {
            using (Context)
            {
                var result = (from userCart in Context.UserCarts
                             join user in Context.Users
                                 on userCart.UserId equals user.Id
                             join purse in Context.Purses
                                 on user.Id equals purse.UserId
                             where userCart.Id == userCartId
                             select new Purse {Id = purse.Id, UserId = user.Id, Money = purse.Money}).SingleOrDefault();
                return Task.FromResult<Purse>(result);
            }
        }
    }
}
