using Core.Persistence.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IProductDal : IRepository<Product>, IAsyncRepository<Product>
    {
    }
}
