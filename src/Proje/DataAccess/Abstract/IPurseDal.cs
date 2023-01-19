using Core.Persistence.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IPurseDal : IRepository<Purse>, IAsyncRepository<Purse>
    {
    }
}
