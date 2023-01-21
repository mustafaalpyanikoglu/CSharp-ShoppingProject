using AutoMapper;
using Business.Features.OrderDetails.Rules;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Business.Services.OrderDetailService;
using Business.Services.PurseService;
using Core;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Services.OrderService
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDetailDal _orderDetailDal;
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDetailDal orderDetailDal, IOrderDal orderDal)
        {
            _orderDetailDal = orderDetailDal;
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

        public async Task<IDataResult<List<OrderDetail>>> ConfirmOrders(int orderId)
        {

            List<OrderDetail> orderDetails = _orderDetailDal.OrdersToBeConfirmed(orderId);
            return new SuccessDataResult<List<OrderDetail>>(orderDetails);
        }
    }
}
