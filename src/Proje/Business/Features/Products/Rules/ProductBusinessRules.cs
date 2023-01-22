using Business.Features.Products.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;

namespace Business.Features.Products.Rules
{
    public class ProductBusinessRules:BaseBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ProductIdShouldExistWhenSelected(int? id)
        {
            Product? result = await _unitOfWork.ProductDal.GetAsync(b => b.Id == id);
            if (result ==null) throw new BusinessException(ProductMessages.ProductNotFound);
        }
        public async Task ProductNameShouldExistWhenSelected(string productName)
        {
            Product? result = await _unitOfWork.ProductDal.GetAsync(b => b.Name == productName);
            if (result == null) throw new BusinessException(ProductMessages.ProductNotFound);
        }
        public async Task ProductNameShouldBeNotExists(string name)
        {
            Product? user = await _unitOfWork.ProductDal.GetAsync(u => u.Name.ToLower() == name.ToLower());
            if (user != null) throw new BusinessException(ProductMessages.ProductNameAlreadyExists);
        }

        public async Task OperationMustBeAvailable()
        {
            List<Product>? results = _unitOfWork.ProductDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(ProductMessages.ProductNotFound);
        }
        public IDataResult<List<Product>> MustBeARegisteredProduct()
        {
            List<Product>? result = _unitOfWork.ProductDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(ProductMessages.ProductNotFound);
            return new SuccessDataResult<List<Product>>(result, ProductMessages.ProductAvaliable);
        }

        public async Task<Product> ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(int id)
        {
            Product? result = await _unitOfWork.ProductDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(ProductMessages.ProductNotFound);
            return result;
        }
    }
}
