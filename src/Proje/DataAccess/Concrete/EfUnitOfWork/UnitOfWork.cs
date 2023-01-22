using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;

namespace DataAccess.Concrete.EfUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseDbContext _baseDbContext;
        public ICategoryDal CategoryDal { get; }
        public IOperationClaimDal OperationClaimDal { get; }
        public IOrderDal OrderDal { get; }
        public IOrderDetailDal OrderDetailDal { get; }
        public IProductDal ProductDal { get; }
        public IPurseDal PurseDal { get; }
        public IUserCartDal UserCartDal { get; }
        public IUserDal UserDal { get; }
        public IUserOperationClaimDal UserOperationClaimDal { get; }

        public UnitOfWork(BaseDbContext baseDbContext, ICategoryDal categoryDal, IOperationClaimDal operationClaimDal,
            IOrderDal orderDal, IOrderDetailDal orderDetailDal, IProductDal productDal, IPurseDal purseDal,
            IUserCartDal userCartDal, IUserDal userDal, IUserOperationClaimDal userOperationClaimDal)
        {
            _baseDbContext = baseDbContext;
            CategoryDal = categoryDal;
            OperationClaimDal = operationClaimDal;
            OrderDal = orderDal;
            OrderDetailDal = orderDetailDal;
            ProductDal = productDal;
            PurseDal = purseDal;
            UserCartDal = userCartDal;
            UserDal = userDal;
            UserOperationClaimDal = userOperationClaimDal;
        }

        public int SaveChanges()
        {
            return _baseDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _baseDbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _baseDbContext.Dispose();
        }
    }
}
