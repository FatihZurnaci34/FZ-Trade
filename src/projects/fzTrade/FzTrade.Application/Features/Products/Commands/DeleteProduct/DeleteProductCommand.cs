using AutoMapper;
using FzTrade.Application.Features.Products.Dtos;
using FzTrade.Application.Features.Products.Rules;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand:IRequest<DeleteProductDto>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHanlder : IRequestHandler<DeleteProductCommand, DeleteProductDto>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;
            private readonly ProductBusinessRules _productBusinessRules;

            public DeleteProductCommandHanlder(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules)
            {
                _mapper = mapper;
                _productRepository = productRepository;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<DeleteProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                Product mappedProduct = _mapper.Map<Product>(request);
                Product deleteProduct = await _productRepository.DeleteAsync(mappedProduct);
                DeleteProductDto deletedProduct = _mapper.Map<DeleteProductDto>(deleteProduct);
                return deletedProduct;
            }
        }
    }
}
