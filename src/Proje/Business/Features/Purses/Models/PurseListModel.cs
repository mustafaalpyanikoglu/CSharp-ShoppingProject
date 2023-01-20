using Business.Features.Purses.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.Purses.Models
{
    public class PurseListModel:BasePageableModel
    {
        public IList<PurseListDto> Items { get; set; }
    }
}
