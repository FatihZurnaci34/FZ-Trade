using AutoMapper;
using FzTrade.Application.Features.Subcategories.Dtos;
using FzTrade.Application.Features.Subcategories.Rules;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Subcategories.Queries.GetByIdSubcategory
{
    public class GetByIdSubcategoryQuery:IRequest<SubcategoryGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdSubcategoryQueryHandler : IRequestHandler<GetByIdSubcategoryQuery, SubcategoryGetByIdDto>
        {
            private readonly ISubcategorRepository _subcategoryRepository;
            private readonly IMapper _mapper;
            private readonly SubcategoryBusinessRules _subcategoryBusinessRules;

            public GetByIdSubcategoryQueryHandler(ISubcategorRepository subcategoryRepository, IMapper mapper, SubcategoryBusinessRules subcategoryBusinessRules)
            {
                _subcategoryRepository = subcategoryRepository;
                _mapper = mapper;
                _subcategoryBusinessRules = subcategoryBusinessRules;
            }

            public async Task<SubcategoryGetByIdDto> Handle(GetByIdSubcategoryQuery request, CancellationToken cancellationToken)
            {
                Subcategory? subcategory = await _subcategoryRepository.GetAsync(t => t.Id == request.Id, include: a => a.Include(m => m.Category));
                SubcategoryGetByIdDto subcategoryGetByIdDto = _mapper.Map<SubcategoryGetByIdDto>(subcategory);
                return subcategoryGetByIdDto;
            }
        }
    }
}
