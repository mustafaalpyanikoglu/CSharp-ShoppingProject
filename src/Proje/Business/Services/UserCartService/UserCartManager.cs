using Business.Features.UserCarts.Rules;
using Business.Features.Users.Rules;
using DataAccess.Abstract;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;

namespace Business.Services.UserCartService
{
    public class UserCartManager:IUserCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserCartBusinessRules _userCartBusinessRules;
        private readonly UserBusinessRules _userBusinessRules;

        public UserCartManager(IUnitOfWork unitOfWork, UserCartBusinessRules userCartBusinessRules, UserBusinessRules userBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _userCartBusinessRules = userCartBusinessRules;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<UserCart> Add(UserCart userCart)
        {
            await _userBusinessRules.UserIdMustBeAvailable(userCart.UserId);

            UserCart addedUserCart = await _unitOfWork.UserCartDal.AddAsync(userCart);

            _unitOfWork.SaveChangesAsync();

            return addedUserCart;
        }
    }
}
