using Business.Features.Users.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.Users.Models
{
    public class UserListModel:BasePageableModel
    {
        public IList<UserListDto> Items { get; set; }
    }
}
