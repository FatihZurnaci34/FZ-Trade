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

namespace FzTrade.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand:IRequest<UpdateProductDto>
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int SubcategoryId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductDto>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;
            private readonly ProductBusinessRules _productBusinessRules;

            public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules)
            {
                _mapper = mapper;
                _productRepository = productRepository;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<UpdateProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                Product mappedProduct = _mapper.Map<Product>(request);
                Product updateProduct = await _productRepository.UpdateAsync(mappedProduct);
                UpdateProductDto updatedProduct = _mapper.Map<UpdateProductDto>(updateProduct);
                return updatedProduct;

            }
        }
    }
}
