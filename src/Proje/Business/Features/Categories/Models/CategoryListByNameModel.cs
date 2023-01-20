using Business.Features.Categories.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.Products.Models
{
    public class CategoryListByNameModel:BasePageableModel
    {
        public IList<CategoryListByNameDto> Items { get; set; }
    }
}
