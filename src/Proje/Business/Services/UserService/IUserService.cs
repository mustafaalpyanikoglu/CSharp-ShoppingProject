using Core.Utilities.Abstract;
using Entities.Concrete;

namespace Business.Services.UserService
{
    public interface IUserService
    {
        Task<IDataResult<User>> GetUserByEmail(string email);
        Task<IDataResult<List<OperationClaim>>> GetClaims(User user);
        Task<IResult> Add(User user);
    }
}
