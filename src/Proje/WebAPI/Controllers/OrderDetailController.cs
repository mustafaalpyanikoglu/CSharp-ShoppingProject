using Business.Features.OrderDetailDetails.Queries.GetListOrderDetailDetail;
using Business.Features.OrderDetailDetails.Queries.GetListOrderDetailDetailByDynamic;
using Business.Features.OrderDetailDetails.Queries.GetListOrderDetailDetailByUserCart;
using Business.Features.OrderDetailDetails.Queries.GetListPastOrderDetailDetail;
using Business.Features.OrderDetails.Commands.CreateOrder;
using Business.Features.OrderDetails.Commands.DeleteOrder;
using Business.Features.OrderDetails.Commands.UpdateOrder;
using Business.Features.OrderDetails.Commands.UpdateOrderDetailForCustomer;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Models;
using Business.Features.OrderDetails.Queries.GetByIdOrderDetail;
using Business.Features.OrderDetails.Queries.GetByOrderDetailByOrderName;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateOrderDetailCommand createOrderDetailCommand)
        {
            CreatedOrderDetailDto result = await Mediator.Send(createOrderDetailCommand);
            return Created("", result);
        }
        [HttpPut("updateproductquantity")]
        public async Task<IActionResult> Add([FromBody] UpdateOrderDetailForCustomerCommand updateOrderDetailForCustomerCommand)
        {
            UpdatedOrderDetailForCustomerDto result = await Mediator.Send(updateOrderDetailForCustomerCommand);
            return Created("", result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderDetailCommand deleteOrderDetailCommand)
        {
            DeletedOrderDetailDto result = await Mediator.Send(deleteOrderDetailCommand);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            UpdatedOrderDetailDto result = await Mediator.Send(updateOrderDetailCommand);
            return Ok(result);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOrderDetailQuery getListOrderDetailQuery = new() { PageRequest = pageRequest };
            OrderDetailListModel result = await Mediator.Send(getListOrderDetailQuery);
            return Ok(result);
        }
        [HttpGet("getlistmyusercartuserid/{userId}")]
        public async Task<IActionResult> GetListOrderByUserCart([FromRoute] int userId, [FromQuery] PageRequest pageRequest)
        {
            GetListOrderDetailByUserCartQuery getListOrderByUserCartQuery = new() { UserId = userId, PageRequest = pageRequest };
            OrderDetailListByUserCartModel result = await Mediator.Send(getListOrderByUserCartQuery);
            return Ok(result);
        }
        [HttpGet("getlistpastorder/{userId}")]
        public async Task<IActionResult> GetListPastOrder([FromRoute] int userId, [FromQuery] PageRequest pageRequest)
        {
            GetListPastOrderDetailQuery getListPastOrderQuery = new() { UserId = userId, PageRequest = pageRequest };
            UserPastOrderListModel result = await Mediator.Send(getListPastOrderQuery);
            return Ok(result);
        }
        [HttpGet("getbyorderbumberorderdetail/{orderNumber}")]
        public async Task<IActionResult> GetListOrderDetailByOrderName([FromRoute] string orderNumber, [FromQuery] PageRequest pageRequest)
        {
            GetListOrderDetailByOrderNameQuery getListOrderDetailByOrderNameQuery = new() { OrderNumber = orderNumber, PageRequest = pageRequest };
            OrderDetailListByUserCartModel result = await Mediator.Send(getListOrderDetailByOrderNameQuery);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdOrderDetailQuery getByIdOrderDetailQuery = new() { Id = id };
            OrderDetailDto result = await Mediator.Send(getByIdOrderDetailQuery);
            return Ok(result);
        }
        [HttpPost("getlist/bydynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
        {
            GetListOrderDetailByDynamicQuery getListOrderDetailByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            OrderDetailListModel result = await Mediator.Send(getListOrderDetailByDynamicQuery);
            return Ok(result);
        }
    }
}
