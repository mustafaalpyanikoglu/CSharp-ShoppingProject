using Business.Features.Purses.Commands.CreatePurse;
using Business.Features.Purses.Commands.DeletePurse;
using Business.Features.Purses.Dtos;
using Business.Features.Purses.Models;
using Business.Features.Purses.Queries.GetByIdPurse;
using Business.Features.Purses.Queries.GetListPurse;
using Business.Features.Purses.Queries.GetListPurseByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurseController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreatePurseCommand createPurseCommand)
        {
            CreatedPurseDto result = await Mediator.Send(createPurseCommand);
            return Created("", result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeletePurseCommand deletePurseCommand)
        {
            DeletedPurseDto result = await Mediator.Send(deletePurseCommand);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePurseCommand updatePurseCommand)
        {
            UpdatedPurseDto result = await Mediator.Send(updatePurseCommand);
            return Ok(result);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPurseQuery getListPurseQuery = new() { PageRequest = pageRequest };
            PurseListModel result = await Mediator.Send(getListPurseQuery);
            return Ok(result);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdPurseQuery getByIdPurseQuery = new GetByIdPurseQuery { Id = id };
            PurseDto result = await Mediator.Send(getByIdPurseQuery);
            return Ok(result);
        }
        [HttpGet("getbyemail/{email}")]
        public async Task<IActionResult> GetByEmail([FromRoute] string email)
        {
            GetByEmailPurseQuery getByIdPurseQuery = new GetByEmailPurseQuery { Email = email };
            PurseDto result = await Mediator.Send(getByIdPurseQuery);
            return Ok(result);
        }
        [HttpPost("getlist/bydynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
        {
            GetListPurseByDynamicQuery getListPurseByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            PurseListModel result = await Mediator.Send(getListPurseByDynamicQuery);
            return Ok(result);
        }
    }
}
