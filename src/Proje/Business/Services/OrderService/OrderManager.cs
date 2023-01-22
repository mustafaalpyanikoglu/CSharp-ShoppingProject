using Core;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;

namespace Business.Services.OrderService
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderManager(IUnitOfWork unitOfWork)
        {
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
