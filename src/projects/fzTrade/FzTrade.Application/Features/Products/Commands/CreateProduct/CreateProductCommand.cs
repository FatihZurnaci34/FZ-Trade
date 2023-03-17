using AutoMapper;
using FzTrade.Application.Features.Products.Dtos;
using FzTrade.Application.Features.Products.Rules;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand:IRequest<CreateProductDto>
    {
        public int SupplierId { get; set; }
        public int SubcategoryId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductDto>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;
            private readonly ProductBusinessRules _productBusinessRules;

            public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules)
            {
                _mapper = mapper;
                _productRepository = productRepository;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<CreateProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                Product mappedProduct = _mapper.Map<Product>(request);
                Product createProduct = await _productRepository.AddAsync(mappedProduct);
                CreateProductDto createdProduct = _mapper.Map<CreateProductDto>(createProduct);
                return createdProduct;
            }
        }
    }
}
