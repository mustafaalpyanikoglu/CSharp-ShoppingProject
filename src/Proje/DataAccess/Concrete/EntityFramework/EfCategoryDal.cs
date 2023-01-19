using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfRepositoryBase<Category, BaseDbContext>, ICategoryDal
    {
        public EfCategoryDal(BaseDbContext context) : base(context)
        {
        }
    }
}
