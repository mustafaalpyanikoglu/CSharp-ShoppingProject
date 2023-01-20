using Business.Features.UserCarts.Rules;
using Business.Features.Users.Rules;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Services.UserCartService
{
    public class UserCartManager:IUserCartService
    {
        private readonly IUserCartDal _userCartDal;
        private readonly UserCartBusinessRules _userCartBusinessRules;
        private readonly UserBusinessRules _userBusinessRules;

        public UserCartManager(IUserCartDal userCartDal, UserCartBusinessRules userCartBusinessRules, UserBusinessRules userBusinessRules)
        {
            _userCartDal = userCartDal;
            _userCartBusinessRules = userCartBusinessRules;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<UserCart> Add(UserCart userCart)
        {
            await _userBusinessRules.UserIdMustBeAvailable(userCart.UserId);

            UserCart addedUserCart = await _userCartDal.AddAsync(userCart);

            return addedUserCart;
        }
    }
}
