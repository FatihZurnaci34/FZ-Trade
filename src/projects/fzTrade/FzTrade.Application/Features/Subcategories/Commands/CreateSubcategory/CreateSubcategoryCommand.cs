using AutoMapper;
using FzTrade.Application.Features.Subcategories.Dtos;
using FzTrade.Application.Features.Subcategories.Rules;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Subcategories.Commands.CreateSubcategory
{
    public class CreateSubcategoryCommand:IRequest<CreateSubcategoryDto>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand, CreateSubcategoryDto>
        {
            private readonly IMapper _mapper;
            private readonly ISubcategorRepository _subcategorRepository;
            private readonly SubcategoryBusinessRules _subcategoryBusinessRules;

            public CreateSubcategoryCommandHandler(IMapper mapper, ISubcategorRepository subcategorRepository, SubcategoryBusinessRules subcategoryBusinessRules)
            {
                _mapper = mapper;
                _subcategorRepository = subcategorRepository;
                _subcategoryBusinessRules = subcategoryBusinessRules;
            }

            public async Task<CreateSubcategoryDto> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
            {
                Subcategory mappedSubcategory = _mapper.Map<Subcategory>(request);
                Subcategory createSubcategory = await _subcategorRepository.AddAsync(mappedSubcategory);
                CreateSubcategoryDto createdSubcategory = _mapper.Map<CreateSubcategoryDto>(createSubcategory);
                return createdSubcategory;

            }
        }
    }
}
