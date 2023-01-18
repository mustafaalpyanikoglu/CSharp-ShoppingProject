using Core.DataAccess.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserOperationClaimDal : IRepository<UserOperationClaim>, IAsyncRepository<UserOperationClaim>
    {
    }
}
