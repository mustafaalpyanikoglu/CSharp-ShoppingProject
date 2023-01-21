using Business.Features.Orders.Commands.ConfirmOrder;
using Business.Features.Orders.Commands.CreateOrder;
using Business.Features.Orders.Commands.DeleteOrder;
using Business.Features.Orders.Commands.UpdateOrder;
using Business.Features.Orders.Commands.UpdateProductQuantityInOrder;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Models;
using Business.Features.Orders.Queries.GetByIdOrder;
using Business.Features.Orders.Queries.GetListOrder;
using Business.Features.Orders.Queries.GetListOrderByDynamic;
using Business.Features.Orders.Queries.GetListPastOrder;
using Business.Features.UserCarts.Dtos;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateOrderCommand createOrderCommand)
        {
            CreatedOrderDto result = await Mediator.Send(createOrderCommand);
            return Created("", result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderCommand deleteOrderCommand)
        {
            DeletedOrderDto result = await Mediator.Send(deleteOrderCommand);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOrderCommand updateOrderCommand)
        {
            UpdatedOrderDto result = await Mediator.Send(updateOrderCommand);
            return Ok(result);
        }
        [HttpPost("confirmorder")]
        public async Task<IActionResult> ConfirmOrder([FromBody] ConfirmOrderCommand confirmOrderCommand)
        {
            ConfirmOrderDto result = await Mediator.Send(confirmOrderCommand);
            return Ok(result);
        }
        [HttpPost("updateproductquantityinorder")]
        public async Task<IActionResult> UpdateProductQuantityInOrder([FromBody] UpdateProductQuantityInOrderCommand updateProductQuantityInOrderCommand)
        {
            UpdateProductQuantityInOrderDto result = await Mediator.Send(updateProductQuantityInOrderCommand);
            return Ok(result);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOrderQuery getListOrderQuery = new() { PageRequest = pageRequest };
            OrderListModel result = await Mediator.Send(getListOrderQuery);
            return Ok(result);
        }
        [HttpGet("GetList/ByUserCartId/{userCartId}")]
        public async Task<IActionResult> GetListOrderByUserCart([FromRoute] int userCartId, [FromQuery] PageRequest pageRequest)
        {
            GetListOrderByUserCartQuery getListOrderByUserCartQuery = new() { UserCartId = userCartId, PageRequest = pageRequest };
            OrderListByUserCartModel result = await Mediator.Send(getListOrderByUserCartQuery);
            return Ok(result);
        }
        [HttpGet("GetList/PastOrder/{userId}")]
        public async Task<IActionResult> GetListPastOrder([FromRoute] int userId, [FromQuery] PageRequest pageRequest)
        {
            GetListPastOrderQuery getListPastOrderQuery = new() {UserId = userId, PageRequest = pageRequest };
            OrderListByUserCartModel result = await Mediator.Send(getListPastOrderQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdOrderQuery getByIdOrderQuery)
        {
            OrderDto result = await Mediator.Send(getByIdOrderQuery);
            return Ok(result);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
        {
            GetListOrderByDynamicQuery getListOrderByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            OrderListModel result = await Mediator.Send(getListOrderByDynamicQuery);
            return Ok(result);
        }
    }
}
