using Core.DataAccess.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IOperationClaimDal : IRepository<OperationClaim>, IAsyncRepository<OperationClaim>
    {
    }
}
