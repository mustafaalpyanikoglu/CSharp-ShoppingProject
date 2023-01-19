using Business.Features.Categories.Commands.CreateCategory;
using Business.Features.Categories.Commands.DeleteCategory;
using Business.Features.Categories.Commands.UpdateCategory;
using Business.Features.Categories.Dtos;
using Business.Features.Categories.Queries.GetByIdCategory;
using Business.Features.Categories.Queries.GetListCategory;
using Business.Features.Categories.Queries.GetListCategoryByDynamic;
using Business.Features.Products.Models;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            CreatedCategoryDto result = await Mediator.Send(createCategoryCommand);
            return Created("", result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand deleteCategoryCommand)
        {
            DeletedCategoryDto result = await Mediator.Send(deleteCategoryCommand);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            UpdatedCategoryDto result = await Mediator.Send(updateCategoryCommand);
            return Ok(result);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCategoryQuery getListCategoryQuery = new() { PageRequest = pageRequest };
            CategoryListModel result = await Mediator.Send(getListCategoryQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCategoryQuery getByIdCategoryQuery)
        {
            CategoryDto result = await Mediator.Send(getByIdCategoryQuery);
            return Ok(result);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
        {
            GetListCategoryByDynamicQuery getListCategoryByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            CategoryListModel result = await Mediator.Send(getListCategoryByDynamicQuery);
            return Ok(result);
        }
    }
}
