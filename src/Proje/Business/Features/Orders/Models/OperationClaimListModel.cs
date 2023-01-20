using Business.Features.Orders.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.Orders.Models
{
    public class OrderListModel: BasePageableModel
    {
        public IList<OrderListDto> Items { get; set; }
    }
}
