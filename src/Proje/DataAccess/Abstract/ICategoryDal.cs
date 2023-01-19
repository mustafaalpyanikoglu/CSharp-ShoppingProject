using Core.Persistence.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IRepository<Category>, IAsyncRepository<Category>
    {
    }
}
