using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfRepositoryBase<UserOperationClaim, BaseDbContext>, IUserOperationClaimDal
    {
        public EfUserOperationClaimDal(BaseDbContext context) : base(context)
        {
        }
    }
}
