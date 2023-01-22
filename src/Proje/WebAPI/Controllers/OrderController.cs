using Business.Features.Orders.Commands.ConfirmOrder;
using Business.Features.Orders.Commands.CreateOrder;
using Business.Features.Orders.Commands.DeleteOrder;
using Business.Features.Orders.Commands.UpdateOrder;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Models;
using Business.Features.Orders.Queries.GetByIdOrder;
using Business.Features.Orders.Queries.GetListOrder;
using Business.Features.Orders.Queries.GetListOrderByDynamic;
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
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOrderQuery getListOrderQuery = new() { PageRequest = pageRequest };
            OrderListModel result = await Mediator.Send(getListOrderQuery);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdOrderQuery getByIdOrderQuery = new() { Id= id };
            OrderDto result = await Mediator.Send(getByIdOrderQuery);
            return Ok(result);
        }
        [HttpPost("getlist/bydynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
        {
            GetListOrderByDynamicQuery getListOrderByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            OrderListModel result = await Mediator.Send(getListOrderByDynamicQuery);
            return Ok(result);
        }
    }
}
