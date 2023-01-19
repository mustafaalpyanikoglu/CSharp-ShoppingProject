using Business.Features.Products.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Features.Products.Rules
{
    public class ProductBusinessRules:BaseBusinessRules
    {
        private readonly IProductDal _ProductDal;

        public ProductBusinessRules(IProductDal ProductDal)
        {
            _ProductDal = ProductDal;
        }

        public async Task ProductIdShouldExistWhenSelected(int? id)
        {
            Product? result = await _ProductDal.GetAsync(b => b.Id == id);
            if (result ==null) throw new BusinessException(ProductMessages.ProductNotFound);
        }
        public async Task ProductNameShouldBeNotExists(string name)
        {
            Product? user = await _ProductDal.GetAsync(u => u.Name.ToLower() == name.ToLower());
            if (user != null) throw new BusinessException(ProductMessages.ProductNameAlreadyExists);
        }

        public async Task OperationMustBeAvailable()
        {
            List<Product>? results = _ProductDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(ProductMessages.ProductNotFound);
        }
        public IDataResult<List<Product>> MustBeARegisteredProduct()
        {
            List<Product>? result = _ProductDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(ProductMessages.ProductNotFound);
            return new SuccessDataResult<List<Product>>(result, ProductMessages.ProductAvaliable);
        }

        public async Task<IDataResult<Product>> ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(int id)
        {
            Product? result = await _ProductDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(ProductMessages.ProductNotFound);
            return new SuccessDataResult<Product>(result, ProductMessages.ProductAvaliable);
        }
    }
}
