using Business.Features.OrderDetails.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.OrderDetails.Models
{
    public class OrderDetailListModel : BasePageableModel
    {
        public IList<OrderDetailListDto> Items { get; set; }
    }
}
