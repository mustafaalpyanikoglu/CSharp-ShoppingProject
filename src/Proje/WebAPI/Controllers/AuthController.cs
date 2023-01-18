using Business.Features.Auths.Dtos;
using Business.Services.AuthService;
using Core.Utilities.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            IDataResult<User> userToLogin = await _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }
            AccessToken result = await _authService.CreateAccessToken(userToLogin.Data);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            IDataResult<User> registerResult = await _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            AccessToken result = await _authService.CreateAccessToken(registerResult.Data);
            return Ok(result);
        }
        [HttpPost("changepassword")]
        public async Task<ActionResult> ChangePassword([FromBody] UserForChangePasswordDto userForChangePasswordDto)
        {
            var result = await _authService.ChangePassword(userForChangePasswordDto);  //istisna bir durum var kullanmak çok yanlış ama IResult yaptığımda kabul etmedi
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
