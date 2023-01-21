using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDetailDal : EfRepositoryBase<OrderDetail, BaseDbContext>, IOrderDetailDal
    {
        public EfOrderDetailDal(BaseDbContext context) : base(context)
        {
        }

        public float CalculateAmountInCart(int orderId)
        {
            using (Context)
            {
                return Context.OrderDetails.Where(o => o.OrderId == orderId).Sum(o => o.TotalPrice);

            }
        }

        public List<OrderDetail> OrdersToBeConfirmed(int orderId)
        {
            using (Context)
            {
                var result = (from orderDetails in Context.OrderDetails
                              join orders in Context.Orders
                                  on orderDetails.OrderId equals orders.Id
                              where orderDetails.OrderId == orderId
                              select new OrderDetail
                              {
                                  Id = orderDetails.Id,
                                  OrderId = orderDetails.OrderId,
                                  ProductId = orderDetails.ProductId,
                                  Quantity = orderDetails.Quantity,
                                  TotalPrice = orderDetails.TotalPrice
                              });
                return result.ToList();
            }
        }
    }
}
