namespace Business.Services.OrderService
{
    public interface IOrderService
    {
        public Task<string> CreateOrderNumber();
    }
}
