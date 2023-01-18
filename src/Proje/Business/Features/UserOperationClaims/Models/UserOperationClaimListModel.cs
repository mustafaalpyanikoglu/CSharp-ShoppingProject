using Business.Features.UserOperationClaims.Dtos;
using Core.DataAccess.Paging;

namespace Business.Features.UserOperationClaims.Models
{
    public class UserOperationClaimListModel : BasePageableModel
    {
        public IList<UserOperationClaimListDto> Items { get; set; }
    }
}
