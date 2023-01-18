using Core.Utilities.Abstract;
using Entities.Concrete;

namespace Business.Services.UserService
{
    public interface IUserService
    {
        Task<IDataResult<User>> GetUserByUserName(string userName);
        Task<IDataResult<List<OperationClaim>>> GetClaims(User user);
        Task<IResult> Add(User user);
        Task<IDataResult<User>> GetByUserName(string userName);
    }
}
