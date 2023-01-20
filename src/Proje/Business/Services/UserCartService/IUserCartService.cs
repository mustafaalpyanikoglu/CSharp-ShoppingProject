using Entities.Concrete;

namespace Business.Services.UserCartService
{
    public interface IUserCartService
    {
        public Task<UserCart> Add(UserCart userCart);
    }
}
