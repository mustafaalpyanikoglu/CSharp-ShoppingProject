using Business.Features.Users.Command.CreateUser;
using Business.Features.Users.Command.DeleteUser;
using Business.Features.Users.Command.UpdateUser;
using Business.Features.Users.Command.UpdateUserFromAuth;
using Business.Features.Users.Dtos;
using Business.Features.Users.Models;
using Business.Features.Users.Queries.GetByIdUser;
using Business.Features.Users.Queries.GetListUser;
using Business.Features.Users.Queries.GetListUserByDynamic;
using Core.Business.Requests;
using Core.DataAccess.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand)
        {
            CreatedUserDto result = await Mediator.Send(createUserCommand);
            return Created("", result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUserCommand)
        {
            DeletedUserDto result = await Mediator.Send(deleteUserCommand);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            UpdatedUserDto result = await Mediator.Send(updateUserCommand);
            return Ok(result);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
            UserListModel result = await Mediator.Send(getListUserQuery);
            return Ok(result);
        }
        [HttpPut("FromAuth")]
        public async Task<IActionResult> UpdateFromAuth([FromBody] UpdateUserFromAuthCommand updateUserFromAuthCommand)
        {
            //updateUserFromAuthCommand.Id = getUserIdFromRequest();
            UpdatedUserFromAuthDto result = await Mediator.Send(updateUserFromAuthCommand);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUserQuery getByIdUserQuery)
        {
            UserDto result = await Mediator.Send(getByIdUserQuery);
            return Ok(result);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
        {
            GetListUserByDynamicQuery listUsersByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            UserListModel result = await Mediator.Send(listUsersByDynamicQuery);
            return Ok(result);
        }
    }
}
