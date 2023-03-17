using Core.Application.Requests;
using Core.Persistence.Dynamic;
using FzTrade.Application.Features.Products.Commands.CreateProduct;
using FzTrade.Application.Features.Products.Commands.DeleteProduct;
using FzTrade.Application.Features.Products.Commands.UpdateProduct;
using FzTrade.Application.Features.Products.Dtos;
using FzTrade.Application.Features.Products.Models;
using FzTrade.Application.Features.Products.Queries.GetByIdProduct;
using FzTrade.Application.Features.Products.Queries.GetListProduct;
using FzTrade.Application.Features.Products.Queries.GetListProducyByDynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace FzTrade.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CreateProductCommand createProductCommand)
        {
            CreateProductDto result = await Mediator.Send(createProductCommand);
            return Created("", result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateProductCommand updateProductCommand)
        {
            UpdateProductDto result = await Mediator.Send(updateProductCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]DeleteProductCommand deleteProductCommand)
        {
            DeleteProductDto result = await Mediator.Send(deleteProductCommand);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQuery getByIdProductQuery)
        {
            ProductGetByIdDto result = await Mediator.Send(getByIdProductQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProductQuery getListProductQuery = new() { PageRequest = pageRequest };
            ProductListModel result = await Mediator.Send(getListProductQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListProductByDynamicQuery getListProductByDynamicQuery = new GetListProductByDynamicQuery { PageRequest = pageRequest, Dynamic = dynamic };
            ProductListModel result = await Mediator.Send(getListProductByDynamicQuery);
            return Ok(result);
        }
    }
}
