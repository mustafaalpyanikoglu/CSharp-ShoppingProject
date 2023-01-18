using Business.Features.OperationClaims.Dtos;
using Core.DataAccess.Paging;

namespace Business.Features.OperationClaims.Models
{
    public class OperationClaimListModel: BasePageableModel
    {
        public IList<OperationClaimListDto> Items { get; set; }
    }
}
