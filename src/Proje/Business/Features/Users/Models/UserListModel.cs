using Business.Features.Users.Dtos;
using Core.DataAccess.Paging;

namespace Business.Features.Users.Models
{
    public class UserListModel:BasePageableModel
    {
        public IList<UserListDto> Items { get; set; }
    }
}
