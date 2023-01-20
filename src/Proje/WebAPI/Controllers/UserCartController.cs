using Business.Features.UserCarts.Commands.CreateUserCart;
using Business.Features.UserCarts.Commands.DeleteUserCart;
using Business.Features.UserCarts.Commands.UpdateUserCart;
using Business.Features.UserCarts.Dtos;
using Business.Features.UserCarts.Models;
using Business.Features.UserCarts.Queries.GetByIdUserCart;
using Business.Features.UserCarts.Queries.GetListUserCart;
using Business.Features.UserCarts.Queries.GetListUserCartByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCartController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateUserCartCommand createUserCartCommand)
        {
            CreatedUserCartDto result = await Mediator.Send(createUserCartCommand);
            return Created("", result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCartCommand deleteUserCartCommand)
        {
            DeletedUserCartDto result = await Mediator.Send(deleteUserCartCommand);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCartCommand updateUserCartCommand)
        {
            UpdatedUserCartDto result = await Mediator.Send(updateUserCartCommand);
            return Ok(result);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserCartQuery getListUserCartQuery = new() { PageRequest = pageRequest };
            UserCartListModel result = await Mediator.Send(getListUserCartQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUserCartQuery getByIdUserCartQuery)
        {
            UserCartDto result = await Mediator.Send(getByIdUserCartQuery);
            return Ok(result);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
        {
            GetListUserCartByDynamicQuery getListUserCartByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            UserCartListModel result = await Mediator.Send(getListUserCartByDynamicQuery);
            return Ok(result);
        }
    }
}
