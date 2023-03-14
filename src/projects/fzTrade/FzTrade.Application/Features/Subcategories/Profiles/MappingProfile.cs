using AutoMapper;
using Core.Persistence.Paging;
using FzTrade.Application.Features.Categories.Commands.CreateCategory;
using FzTrade.Application.Features.Subcategories.Commands.CreateSubcategory;
using FzTrade.Application.Features.Subcategories.Commands.DeleteSubcategory;
using FzTrade.Application.Features.Subcategories.Commands.UpdateSubcategory;
using FzTrade.Application.Features.Subcategories.Dtos;
using FzTrade.Application.Features.Subcategories.Models;
using FzTrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Subcategories.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Subcategory, CreateSubcategoryDto>().ReverseMap();
            CreateMap<Subcategory,CreateSubcategoryCommand>().ReverseMap();
            
            CreateMap<Subcategory,UpdateSubcategoryDto>().ReverseMap();
            CreateMap<Subcategory, UpdateSubcategoryCommand>().ReverseMap();


            CreateMap<Subcategory,DeleteSubcategoryDto>().ReverseMap();
            CreateMap<Subcategory, DeleteSubcategoryCommand>().ReverseMap();


            CreateMap<Subcategory, SubcategoryGetByIdDto>().ForMember(s=>s.CategoryName,opt=>opt.MapFrom(s=>s.Category.Name)).ReverseMap();

            CreateMap<Subcategory, SubcategoryListDto>().ForMember(s=>s.CategoryName,opt=>opt.MapFrom(s=>s.Category.Name)).ReverseMap();
            CreateMap<IPaginate<Subcategory>, SubcategoryListModel>().ReverseMap();
        }
    }
}
