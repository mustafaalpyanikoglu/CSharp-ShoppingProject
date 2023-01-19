using Business.Features.Categories.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.Products.Models
{
    public class CategoryListModel:BasePageableModel
    {
        public IList<CategoryListDto> Items { get; set; }
    }
}
