using AutoMapper;
using Business.Features.Products.Dtos;
using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using static Business.Features.Products.Constants.ProductMessages;

namespace Business.Services.ProductService
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ProductListByNameDto> GetListProductByName(string productName)
        {
            List<Product> products = _unitOfWork.ProductDal.GetAll(p=> p.Name == productName);
            if (products.Count <= 0) throw new BusinessException(ProductNotFound);

            ProductListByNameDto productListByNameDto = _mapper.Map<ProductListByNameDto>(products);
            return Task.FromResult<ProductListByNameDto>(productListByNameDto);
        }
    }
}
