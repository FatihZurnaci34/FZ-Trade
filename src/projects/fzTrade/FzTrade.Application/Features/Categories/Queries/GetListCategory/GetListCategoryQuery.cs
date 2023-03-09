using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using FzTrade.Application.Features.Categories.Models;
using FzTrade.Application.Features.Categories.Rules;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Categories.Queries.GetListCategory
{
    public class GetListCategoryQuery:IRequest<CategoryListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, CategoryListModel>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _categoryRepository;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public GetListCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
            {
                _mapper = mapper;
                _categoryRepository = categoryRepository;
                _categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<CategoryListModel> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Category> category = await _categoryRepository.GetListAsync(index:request.PageRequest.Page,size:request.PageRequest.PageSize);
                CategoryListModel mappedCategory = _mapper.Map<CategoryListModel>(category);
                return mappedCategory;
            }
        }
    }
}
