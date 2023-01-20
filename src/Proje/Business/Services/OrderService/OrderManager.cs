using Core;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Services.OrderService
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public async Task<string> CreateOrderNumber()
        {
            string randomOrderNumber;
            while (true)
            {
                randomOrderNumber = RandomNumberHelper.CreateRandomNumberHelper();
                Order? order = await _orderDal.GetAsync(o => o.OrderNumber == randomOrderNumber);
                if (order == null) break;
            }
            return randomOrderNumber;
        }
    }
}
