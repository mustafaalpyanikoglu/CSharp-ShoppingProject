using Business.Features.OrderDetails.Dtos;
using Business.Services.OrderService;
using Core.Persistence.Paging;

namespace Business.Features.OrderDetails.Models
{
    public class OrderDetailListByUserCartModel : BasePageableModel
    {
        public IList<OrderDetailListDtoForCustomer> Items { get; set; }
    }
}
