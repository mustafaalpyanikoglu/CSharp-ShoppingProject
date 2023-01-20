using Business.Features.UserCarts.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Features.UserCarts.Rules
{
    public class UserCartBusinessRules : BaseBusinessRules
    {
        private readonly IUserCartDal _userCartDal;

        public UserCartBusinessRules(IUserCartDal userCartDal)
        {
            _userCartDal = userCartDal;
        }

        public async Task UserCartIdShouldExistWhenSelected(int? id)
        {
            UserCart? result = await _userCartDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(UserCartMessages.UserCartNotFound);
        }

        public async Task OperationMustBeAvailable()
        {
            List<UserCart>? results = _userCartDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(UserCartMessages.UserCartNotFound);
        }
        public IDataResult<List<UserCart>> MustBeARegisteredUserCart()
        {
            List<UserCart>? result = _userCartDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(UserCartMessages.UserCartNotFound);
            return new SuccessDataResult<List<UserCart>>(result, UserCartMessages.UserCartAvaliable);
        }

        public async Task<IDataResult<UserCart>> ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(int id)
        {
            UserCart? result = await _userCartDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(UserCartMessages.UserCartNotFound);
            return new SuccessDataResult<UserCart>(result, UserCartMessages.UserCartAvaliable);
        }
    }
}
