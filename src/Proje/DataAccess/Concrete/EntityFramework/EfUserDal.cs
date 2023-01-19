using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfRepositoryBase<User, BaseDbContext>, IUserDal
    {
        public EfUserDal(BaseDbContext context) : base(context)
        {
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (Context)
            {
                var result = from operationClaim in Context.OperationClaims
                             join userOperationClaim in Context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}
