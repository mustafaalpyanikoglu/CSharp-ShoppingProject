using DataAccess.Abstract;

namespace DataAccess.Concrete.Contexts
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryDal CategoryDal { get; }
        IOperationClaimDal OperationClaimDal { get; }
        IOrderDal OrderDal { get; }
        IOrderDetailDal OrderDetailDal { get; }
        IProductDal ProductDal { get; }
        IPurseDal PurseDal { get; }
        IUserCartDal UserCartDal { get; }
        IUserDal UserDal { get; }
        IUserOperationClaimDal UserOperationClaimDal { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        
    }
}
