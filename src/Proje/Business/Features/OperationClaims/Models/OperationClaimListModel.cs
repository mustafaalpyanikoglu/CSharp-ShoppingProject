using Business.Features.OperationClaims.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.OperationClaims.Models
{
    public class OperationClaimListModel: BasePageableModel
    {
        public IList<OperationClaimListDto> Items { get; set; }
    }
}
