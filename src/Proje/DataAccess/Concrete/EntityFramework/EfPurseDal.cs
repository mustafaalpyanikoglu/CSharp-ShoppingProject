using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPurseDal : EfRepositoryBase<Purse, BaseDbContext>, IPurseDal
    {
        public EfPurseDal(BaseDbContext context) : base(context)
        {
        }
    }
}
