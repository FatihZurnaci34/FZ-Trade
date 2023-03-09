using AutoMapper;
using Core.Persistence.Paging;
using FzTrade.Application.Features.Categories.Commands.CreateCategory;
using FzTrade.Application.Features.Categories.Commands.DeleteCategory;
using FzTrade.Application.Features.Categories.Commands.UpdateCategory;
using FzTrade.Application.Features.Categories.Dtos;
using FzTrade.Application.Features.Categories.Models;
using FzTrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Categories.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category,CreateCategoryCommand>().ReverseMap();

            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category,UpdateCategoryCommand>().ReverseMap();

            CreateMap<Category,  DeleteCategoryDto>().ReverseMap();
            CreateMap<Category, DeleteCategoryCommand>().ReverseMap();

            CreateMap<Category, CategoryGetByIdDto>().ReverseMap();
            

            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<IPaginate<Category>,CategoryListModel>().ReverseMap();
        }
    }
}
