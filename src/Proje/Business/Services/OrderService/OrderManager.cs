using Core;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace Business.Services.OrderService
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDetailDal _orderDetailDal;
        private readonly IOrderDal _orderDal;
        private readonly IUnitOfWork _unitOfWork;

        public OrderManager(IOrderDetailDal orderDetailDal, IOrderDal orderDal, IUnitOfWork unitOfWork)
        {
            _orderDetailDal = orderDetailDal;
            _orderDal = orderDal;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreateOrderNumber()
        {
            string randomOrderNumber;
            while (true)
            {
                randomOrderNumber = RandomNumberHelper.CreateRandomNumberHelper();
                Order? order = await _unitOfWork.OrderDal.GetAsync(o => o.OrderNumber == randomOrderNumber);
                if (order == null) break;
            }
            return randomOrderNumber;
        }

        public async Task<IDataResult<List<OrderDetail>>> ConfirmOrders(int orderId)
        {

            List<OrderDetail> orderDetails = _unitOfWork.OrderDetailDal.OrdersToBeConfirmed(orderId);
            return new SuccessDataResult<List<OrderDetail>>(orderDetails);
        }
    }
}
