using Business.Features.Products.Dtos;

namespace Business.Services.ProductService
{
    public interface IProductService
    {
        public Task<ProductListByNameDto> GetListProductByName(string productName);
    }
}
