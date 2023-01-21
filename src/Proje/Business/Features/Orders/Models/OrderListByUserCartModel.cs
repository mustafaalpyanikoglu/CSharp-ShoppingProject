using Business.Features.Orders.Dtos;
using Business.Services.OrderService;
using Core.Persistence.Paging;

namespace Business.Features.Orders.Models
{
    public class OrderListByUserCartModel : BasePageableModel
    {
        public IList<OrderListDtoForCustomer> Items { get; set; }
    }
}
