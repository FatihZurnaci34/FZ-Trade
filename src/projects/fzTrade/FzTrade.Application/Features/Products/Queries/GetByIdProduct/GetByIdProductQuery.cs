using AutoMapper;
using FzTrade.Application.Features.Products.Dtos;
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

namespace FzTrade.Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQuery:IRequest<ProductGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ProductGetByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;
            private readonly ProductBusinessRules _productBusinessRules;

            public GetByIdProductQueryHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules)
            {
                _mapper = mapper;
                _productRepository = productRepository;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<ProductGetByIdDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                Product? product = await _productRepository.GetAsync(p => p.Id == request.Id, include: p => p.Include(p => p.Supplier).Include(p=>p.Subcategory));
                ProductGetByIdDto productGetByIdDto = _mapper.Map<ProductGetByIdDto>(product);
                return productGetByIdDto;
            }
        }
    }
}
