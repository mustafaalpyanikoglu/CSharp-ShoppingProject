﻿using Business.Features.Products.Commands.CreateProduct;
using Business.Features.Products.Commands.DeleteProduct;
using Business.Features.Products.Commands.UpdateProduct;
using Business.Features.Products.Dtos;
using Business.Features.Products.Models;
using Business.Features.Products.Queries.GetByIdProduct;
using Business.Features.Products.Queries.GetListProduct;
using Business.Features.Products.Queries.GetListProductByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateProductCommand createProductCommand)
        {
            CreatedProductDto result = await Mediator.Send(createProductCommand);
            return Created("", result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand deleteProductCommand)
        {
            DeletedProductDto result = await Mediator.Send(deleteProductCommand);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand updateProductCommand)
        {
            UpdatedProductDto result = await Mediator.Send(updateProductCommand);
            return Ok(result);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProductQuery getListProductQuery = new() { PageRequest = pageRequest };
            ProductListModel result = await Mediator.Send(getListProductQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQuery getByIdProductQuery)
        {
            ProductDto result = await Mediator.Send(getByIdProductQuery);
            return Ok(result);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
        {
            GetListProductByDynamicQuery getListProductByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            ProductListModel result = await Mediator.Send(getListProductByDynamicQuery);
            return Ok(result);
        }
    }
}