using Business.Features.Categorys.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;

namespace Business.Features.Categorys.Rules
{
    public class CategoryBusinessRules:BaseBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CategoryIdShouldExistWhenSelected(int? id)
        {
            Category? result = await _unitOfWork.CategoryDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(CategoryMessages.CategoryNotFound);
        }
        public async Task CategoryNameShouldExistWhenSelected(string categoryName)
        {
            Category? result = await _unitOfWork.CategoryDal.GetAsync(b => b.Name == categoryName);
            if (result == null) throw new BusinessException(CategoryMessages.CategoryNotFound);
        }
        public async Task CategoryNameShouldBeNotExists(string name)
        {
            Category? user = await _unitOfWork.CategoryDal.GetAsync(u => u.Name.ToLower() == name.ToLower());
            if (user != null) throw new BusinessException(CategoryMessages.CategoryNameAlreadyExists);
        }

        public async Task OperationMustBeAvailable()
        {
            List<Category>? results = _unitOfWork.CategoryDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(CategoryMessages.CategoryNotFound);
        }
        public IDataResult<List<Category>> MustBeARegisteredCategory()
        {
            List<Category>? result = _unitOfWork.CategoryDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(CategoryMessages.CategoryNotFound);
            return new SuccessDataResult<List<Category>>(result, CategoryMessages.CategoryAvaliable);
        }

        public async Task<IDataResult<Category>> ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(int id)
        {
            Category? result = await _unitOfWork.CategoryDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(CategoryMessages.CategoryNotFound);
            return new SuccessDataResult<Category>(result, CategoryMessages.CategoryAvaliable);
        }
    }
}
