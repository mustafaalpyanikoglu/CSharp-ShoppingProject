using Business.Features.Auths.Commands.ChangePassword;
using Business.Features.Auths.Dtos;
using Business.Features.Categories.Commands.CreateCategory;
using Business.Features.Categories.Dtos;
using Business.Services.AuthService;
using Core.Security.Jwt;
using Core.Utilities.Abstract;
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
        public async Task<IActionResult> Add([FromBody] ChangePasswordCommand changePasswordCommand)
        {
            UserForChangePasswordDto result = await Mediator.Send(changePasswordCommand);
            return Ok(result);
        }
    }
}
