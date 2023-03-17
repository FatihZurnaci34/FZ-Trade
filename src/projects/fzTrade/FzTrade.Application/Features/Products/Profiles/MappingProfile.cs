using AutoMapper;
using Core.Persistence.Paging;
using FzTrade.Application.Features.Products.Commands.CreateProduct;
using FzTrade.Application.Features.Products.Commands.DeleteProduct;
using FzTrade.Application.Features.Products.Commands.UpdateProduct;
using FzTrade.Application.Features.Products.Dtos;
using FzTrade.Application.Features.Products.Models;
using FzTrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Products.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();

            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();


            CreateMap<Product,  DeleteProductDto>().ReverseMap();
            CreateMap<Product, DeleteProductCommand>().ReverseMap();

            CreateMap<Product,ProductGetByIdDto>().ForMember(p=>p.SupplierName, opt=> opt.MapFrom(p=>p.Supplier.CompanyName))
                                                  .ForMember(p=>p.SubcategoryName,opt=>opt.MapFrom(p=>p.Subcategory.Name)).ReverseMap();
            CreateMap<Product,ProductGetListDto>().ForMember(p=>p.SupplierName,opt=>opt.MapFrom(p=>p.Supplier.CompanyName))
                                                  .ForMember(p=>p.SubcategoryName,opt=>opt.MapFrom(p=>p.Subcategory.Name)).ReverseMap();
            CreateMap<IPaginate<Product>, ProductListModel>().ReverseMap();
        }
    }
}
