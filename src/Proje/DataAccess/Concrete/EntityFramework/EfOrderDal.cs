using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfRepositoryBase<Order, BaseDbContext>, IOrderDal
    {
        public EfOrderDal(BaseDbContext context) : base(context)
        {
        }
    }
}
