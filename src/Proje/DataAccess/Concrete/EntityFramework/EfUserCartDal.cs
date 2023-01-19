using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserCartDal : EfRepositoryBase<UserCart, BaseDbContext>, IUserCartDal
    {
        public EfUserCartDal(BaseDbContext context) : base(context)
        {
        }
    }
}
