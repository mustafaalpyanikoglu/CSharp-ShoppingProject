using Business.Features.Orders.Dtos;
using Core.Utilities.Abstract;
using Entities.Concrete;

namespace Business.Services.OrderDetailService
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> ListOrdersToBeConfirmed(int orderId);
        Task<float> AmountUserCart(int orderId);
    }
}
