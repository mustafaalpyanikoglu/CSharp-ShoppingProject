using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfRepositoryBase<Product, BaseDbContext>, IProductDal
    {
        public EfProductDal(BaseDbContext context) : base(context)
        {
        }
    }
}
