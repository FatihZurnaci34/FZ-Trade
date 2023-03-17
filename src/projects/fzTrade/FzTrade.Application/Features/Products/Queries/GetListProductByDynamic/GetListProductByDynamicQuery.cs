using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using FzTrade.Application.Features.Products.Models;
using FzTrade.Application.Features.Products.Rules;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Products.Queries.GetListProducyByDynamic
{
    public class GetListProductByDynamicQuery:IRequest<ProductListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListProductByDynamicQueryHandler : IRequestHandler<GetListProductByDynamicQuery, ProductListModel>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;
            private readonly ProductBusinessRules _productBusinessRules;

            public GetListProductByDynamicQueryHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules)
            {
                _mapper = mapper;
                _productRepository = productRepository;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<ProductListModel> Handle(GetListProductByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Product> product = await _productRepository.GetListByDynamicAsync(request.Dynamic, index: request.PageRequest.Page,
                                                                                            size: request.PageRequest.PageSize,
                                                                                            include: p => p.Include(p=>p.Supplier).Include(p=>p.Subcategory));
                ProductListModel mappedProduct = _mapper.Map<ProductListModel>(product);
                return mappedProduct;
            }
        }
    }
}
