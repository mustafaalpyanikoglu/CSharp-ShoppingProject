using Business.Features.OperationClaims.Commands.CreateOperationClaim;
using Business.Features.OperationClaims.Commands.DeleteOperationClaim;
using Business.Features.OperationClaims.Commands.UpdateOperationClaim;
using Business.Features.OperationClaims.Dtos;
using Business.Features.OperationClaims.Models;
using Business.Features.OperationClaims.Queries.GetByIdOperationClaim;
using Business.Features.OperationClaims.Queries.GetListOperationClaim;
using Business.Features.OperationClaims.Queries.GetListOperationClaimByDynamic;
using Core.Business.Requests;
using Core.DataAccess.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreatedOperationClaimDto result = await Mediator.Send(createOperationClaimCommand);
            return Created("", result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            DeletedOperationClaimDto result = await Mediator.Send(deleteOperationClaimCommand);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            UpdatedOperationClaimDto result = await Mediator.Send(updateOperationClaimCommand);
            return Ok(result);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdOperationClaimQuery getByIdOperationClaimQuery)
        {
            OperationClaimDto result = await Mediator.Send(getByIdOperationClaimQuery);
            return Ok(result);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
        {
            GetListOperationClaimByDynamicQuery getListOperationClaimByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimByDynamicQuery);
            return Ok(result);
        }
    }
}
