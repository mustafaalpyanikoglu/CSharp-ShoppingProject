using Business.Features.OrderDetails.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.OrderDetails.Models
{
    public class UserPastOrderListModel : BasePageableModel
    {
        public IList<UserPastOrderListDto> Items { get; set; }
    }
}
