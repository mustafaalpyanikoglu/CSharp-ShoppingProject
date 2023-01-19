using Business.Features.UserOperationClaims.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.UserOperationClaims.Models
{
    public class UserOperationClaimListModel : BasePageableModel
    {
        public IList<UserOperationClaimListDto> Items { get; set; }
    }
}
