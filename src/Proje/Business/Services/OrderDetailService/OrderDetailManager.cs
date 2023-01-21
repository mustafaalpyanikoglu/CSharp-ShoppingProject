using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Services.OrderDetailService
{
    public class OrderDetailManager:IOrderDetailService
    {
        private readonly IOrderDetailDal _orderDetailDal;

        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public async Task<List<OrderDetail>> ListOrdersToBeConfirmed(int orderId)
        {
            return _orderDetailDal.GetAll(o=>o.OrderId== orderId);
        }
        
    }
}
