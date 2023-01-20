using AutoMapper;
using Business.Features.Products.Dtos;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using static Business.Features.Products.Constants.ProductMessages;

namespace Business.Services.ProductService
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        public Task<ProductListByNameDto> GetListProductByName(string productName)
        {
            List<Product> products = _productDal.GetAll(p=> p.Name == productName);
            if (products.Count <= 0) throw new BusinessException(ProductNotFound);

            ProductListByNameDto productListByNameDto = _mapper.Map<ProductListByNameDto>(products);
            return Task.FromResult<ProductListByNameDto>(productListByNameDto);
        }
    }
}
