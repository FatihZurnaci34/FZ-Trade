using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using FzTrade.Application.Features.Categories.Models;
using FzTrade.Application.Features.Categories.Rules;
using FzTrade.Application.Features.Subcategories.Models;
using FzTrade.Application.Features.Subcategories.Rules;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Subcategories.Queries.GetListSubcategory
{
    public class GetListSubcategoryQuery : IRequest<SubcategoryListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListCategoryQueryHandler : IRequestHandler<GetListSubcategoryQuery, SubcategoryListModel>
        {
            private readonly IMapper _mapper;
            private readonly ISubcategorRepository _subcategorRepository;
            private readonly SubcategoryBusinessRules _subcategoryBusinessRules;

            public GetListCategoryQueryHandler(IMapper mapper, ISubcategorRepository subcategorRepository, SubcategoryBusinessRules subcategoryBusinessRules)
            {
                _mapper = mapper;
                _subcategorRepository = subcategorRepository;
                _subcategoryBusinessRules = subcategoryBusinessRules;
            }

            public async Task<SubcategoryListModel> Handle(GetListSubcategoryQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Subcategory> subcategory = await _subcategorRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize,include:a=>a.Include(s=>s.Category));
                SubcategoryListModel mappedSubcategory = _mapper.Map<SubcategoryListModel>(subcategory);
                return mappedSubcategory;
            }
        }
    }
}
