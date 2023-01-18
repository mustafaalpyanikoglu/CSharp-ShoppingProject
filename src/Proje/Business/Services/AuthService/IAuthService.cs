using Business.Features.Auths.Dtos;
using Core.Utilities.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;

namespace Business.Services.AuthService
{
    public interface IAuthService
    {
        Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password);
        Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto);
        Task<IResult> UserExists(string userName);
        Task<IResult> ChangePassword(UserForChangePasswordDto userForChangePasswordDto);
        Task<AccessToken> CreateAccessToken(User user);
    }
}
