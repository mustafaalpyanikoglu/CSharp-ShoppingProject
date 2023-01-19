using Core.Persistence.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal:IRepository<User>,IAsyncRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
