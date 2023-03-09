using Core.Application.Requests;
using FzTrade.Application.Features.Categories.Commands.CreateCategory;
using FzTrade.Application.Features.Categories.Commands.DeleteCategory;
using FzTrade.Application.Features.Categories.Commands.UpdateCategory;
using FzTrade.Application.Features.Categories.Dtos;
using FzTrade.Application.Features.Categories.Models;
using FzTrade.Application.Features.Categories.Queries.GetByIdCategory;
using FzTrade.Application.Features.Categories.Queries.GetListCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FzTrade.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            CreateCategoryDto result = await Mediator.Send(createCategoryCommand);
            return Created("",result);

        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            UpdateCategoryDto result = await Mediator.Send(updateCategoryCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand deleteCategoryCommand)
        {
            DeleteCategoryDto result = await Mediator.Send(deleteCategoryCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCategoryQuery getListCategoryQuery = new() { PageRequest = pageRequest };
            CategoryListModel result = await Mediator.Send(getListCategoryQuery);
            return Ok(result);
            
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCategoryQuery getByIdCategoryQuery)
        {
            CategoryGetByIdDto categoryGetByIdDto = await Mediator.Send(getByIdCategoryQuery);
            return Ok(categoryGetByIdDto);
        }
    }
}