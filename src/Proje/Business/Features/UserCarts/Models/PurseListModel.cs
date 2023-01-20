using Business.Features.UserCarts.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.UserCarts.Models
{
    public class UserCartListModel:BasePageableModel
    {
        public IList<UserCartListDto> Items { get; set; }
    }
}
