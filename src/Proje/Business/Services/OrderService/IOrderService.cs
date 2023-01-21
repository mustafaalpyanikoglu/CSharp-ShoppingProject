using Business.Features.Orders.Dtos;
using Core.Utilities.Abstract;
using Entities.Concrete;

namespace Business.Services.OrderService
{
    public interface IOrderService
    {
        public Task<string> CreateOrderNumber();

        Task<IDataResult<List<OrderDetail>>> ConfirmOrders(int orderID);
    }
}
