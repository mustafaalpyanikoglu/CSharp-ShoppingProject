using Core.Persistence.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserCartDal : IRepository<UserCart>, IAsyncRepository<UserCart>
    {
    }
}
