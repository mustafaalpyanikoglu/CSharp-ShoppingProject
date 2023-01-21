using Core.Persistence.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IOrderDetailDal : IRepository<OrderDetail>, IAsyncRepository<OrderDetail>
    {
        float CalculateAmountInCart(int orderId);
        List<OrderDetail> OrdersToBeConfirmed(int orderId);
    }
}
