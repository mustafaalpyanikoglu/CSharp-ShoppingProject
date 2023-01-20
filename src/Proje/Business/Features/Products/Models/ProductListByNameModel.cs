using Business.Features.Products.Dtos;
using Core.Persistence.Paging;

namespace Business.Features.Products.Models
{
    public class ProductListByNameModel:BasePageableModel
    {
        public IList<ProductListByNameDto> Items { get; set; }
    }
}
