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

namespace FzTrade.Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQuery:IRequest<CategoryGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryGetByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _categoryRepository;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public GetByIdCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
            {
                _mapper = mapper;
                _categoryRepository = categoryRepository;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<CategoryGetByIdDto> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
            {
                Category? category = await _categoryRepository.GetAsync(c => c.Id == request.Id);
                CategoryGetByIdDto categoryGetByIdDto = _mapper.Map<CategoryGetByIdDto>(category);
                return categoryGetByIdDto;
            }
        }
    }
}
