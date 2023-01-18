using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal : EfRepositoryBase<OperationClaim, BaseDbContext>, IOperationClaimDal
    {
        public EfOperationClaimDal(BaseDbContext context) : base(context)
        {
        }
    }
}
