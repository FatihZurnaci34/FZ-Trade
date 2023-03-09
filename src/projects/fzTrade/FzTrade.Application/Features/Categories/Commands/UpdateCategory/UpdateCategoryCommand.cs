using AutoMapper;
using Core.Security.Entities;
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

namespace FzTrade.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand:IRequest<UpdateCategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryDto>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<UpdateCategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                Category? category = await _categoryRepository.GetAsync(c => c.Id == request.Id);
                Category? mappedCategory = _mapper.Map(request, category);
                Category? updatedCategory = await _categoryRepository.UpdateAsync(mappedCategory);
                UpdateCategoryDto updateCategory = _mapper.Map<UpdateCategoryDto>(updatedCategory);
                return updateCategory;

            }
        }
    }
}
