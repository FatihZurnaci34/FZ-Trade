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

namespace FzTrade.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand:IRequest<DeleteCategoryDto>
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryDto>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<DeleteCategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                Category mappedCategory = _mapper.Map<Category>(request);
                Category deletedCategory = await _categoryRepository.DeleteAsync(mappedCategory);
                DeleteCategoryDto deleteCategory = _mapper.Map<DeleteCategoryDto>(deletedCategory);
                return deleteCategory;

            }
        }
    }
}
