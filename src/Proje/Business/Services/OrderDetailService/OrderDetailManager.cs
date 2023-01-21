using Business.Features.Orders.Constants;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.OrderDetailService
{
    public class OrderDetailManager:IOrderDetailService
    {
        private readonly IOrderDetailDal _orderDetailDal;

        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public async Task<float> AmountUserCart(int orderId)
        {
            float totalPrice = 0;
            IPaginate<OrderDetail> orderDetails = await _orderDetailDal.GetListAsync(
                    o => o.OrderId == orderId,
                    include: c => c.Include(c => c.Product)
                                   .Include(c => c.Product.Category)
                                   .Include(c => c.Order)
                                   .Include(c => c.Order.UserCart)
                                   .Include(c => c.Order.UserCart.User)
                );
            List<Product> products = new List<Product>();
            foreach (var item in orderDetails.Items)  //sepetin tutarı hesaplanır
            {
                totalPrice += item.TotalPrice;
            }
            return totalPrice;
        }

        public async Task<List<OrderDetail>> ListOrdersToBeConfirmed(int orderId)
        {
            return _orderDetailDal.GetAll(o=>o.OrderId== orderId);
        }
        
    }
}
