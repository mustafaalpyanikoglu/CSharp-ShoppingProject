using Core.Persistence.Repositories;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfRepositoryBase<Order, BaseDbContext>, IOrderDal
    {
        public EfOrderDal(BaseDbContext context) : base(context)
        {
        }

        //public List<OrderListDtoForClient> GetListOrderByUserCart(int userCartId)
        //{
        //    using (Context)
        //    {
        //        var result = from order in Context.Orders
        //                     join product in Context.Products
        //                         on order.ProductId equals product.Id
        //                     where order.UserCartId == userCartId
        //                     select new OrderListDtoForClient
        //                     {
        //                         ProductName = product.Name,
        //                         ProductPrice = product.Price,
        //                         OrderNumber= order.OrderNumber,
        //                         Quantity= order.Quantity,
        //                         TotalPrice = order.TotalPrice
        //                     };
        //        return result.ToList();
        //    }
        //}
    }
        
}
