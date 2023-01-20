using Business.Features.Categories.Dtos;
using Business.Features.Orders.Commands.ConfirmOrder;
using Business.Features.Orders.Commands.CreateOrder;
using Business.Features.Orders.Commands.DeleteOrder;
using Business.Features.Orders.Commands.UpdateOrder;
using Business.Features.Orders.Commands.UpdateProductQuantityInOrder;
using Business.Features.Orders.Dtos;
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
    }
}
