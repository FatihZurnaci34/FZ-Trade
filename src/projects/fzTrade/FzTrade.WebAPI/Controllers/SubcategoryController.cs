using Core.Application.Requests;
using FzTrade.Application.Features.Categories.Commands.UpdateCategory;
using FzTrade.Application.Features.Categories.Models;
using FzTrade.Application.Features.Categories.Queries.GetListCategory;
using FzTrade.Application.Features.Subcategories.Commands.CreateSubcategory;
using FzTrade.Application.Features.Subcategories.Commands.DeleteSubcategory;
using FzTrade.Application.Features.Subcategories.Commands.UpdateSubcategory;
using FzTrade.Application.Features.Subcategories.Dtos;
using FzTrade.Application.Features.Subcategories.Models;
using FzTrade.Application.Features.Subcategories.Queries.GetByIdSubcategory;
using FzTrade.Application.Features.Subcategories.Queries.GetListSubcategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FzTrade.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SubcategoryController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSubcategoryCommand createSubcategoryCommand)
        {
            CreateSubcategoryDto result = await Mediator.Send(createSubcategoryCommand);
            return Created("", result);
        }


        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateSubcategoryCommand updateSubcategoryCommand)
        {
            UpdateSubcategoryDto result = await Mediator.Send(updateSubcategoryCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] DeleteSubcategoryCommand deleteSubcategoryCommand)
        {
            DeleteSubcategoryDto result = await Mediator.Send(deleteSubcategoryCommand);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdSubcategoryQuery getByIdSubcategoryQuery)
        {
            SubcategoryGetByIdDto result = await Mediator.Send(getByIdSubcategoryQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSubcategoryQuery getListSubcategoryQuery = new() { PageRequest = pageRequest };
            SubcategoryListModel result = await Mediator.Send(getListSubcategoryQuery);
            return Ok(result);
        }
    }
}
