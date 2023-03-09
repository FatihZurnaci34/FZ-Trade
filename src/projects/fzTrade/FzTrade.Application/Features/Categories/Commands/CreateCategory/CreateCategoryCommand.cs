using AutoMapper;
using FzTrade.Application.Features.Categories.Dtos;
using FzTrade.Application.Features.Categories.Rules;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand:IRequest<CreateCategoryDto>
    {
        public string Name { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryDto>
        {

            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<CreateCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                Category mappedCategory = _mapper.Map<Category>(request);
                Category createdCategory = await _categoryRepository.AddAsync(mappedCategory);
                CreateCategoryDto createCategory = _mapper.Map<CreateCategoryDto>(createdCategory);
                return createCategory;

            }
        }
    }
}
