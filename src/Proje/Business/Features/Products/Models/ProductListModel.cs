using Business.Features.Products.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.Products.Models
{
    public class ProductListModel:BasePageableModel
    {
        public IList<ProductListDto> Items { get; set; }
    }
}
