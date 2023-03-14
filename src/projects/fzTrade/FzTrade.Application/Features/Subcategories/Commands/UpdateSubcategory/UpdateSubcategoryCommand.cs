using AutoMapper;
using FzTrade.Application.Features.Subcategories.Dtos;
using FzTrade.Application.Features.Subcategories.Rules;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Subcategories.Commands.UpdateSubcategory
{
    public class UpdateSubcategoryCommand:IRequest<UpdateSubcategoryDto>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public class UpdateSubcategoryCommandHandler : IRequestHandler<UpdateSubcategoryCommand, UpdateSubcategoryDto>
        {
            private readonly ISubcategorRepository _subcategoryRepository;
            private readonly IMapper _mapper;
            private readonly SubcategoryBusinessRules _subcategoryBusinessRules;

            public UpdateSubcategoryCommandHandler(ISubcategorRepository subcategoryRepository, IMapper mapper, SubcategoryBusinessRules subcategoryBusinessRules)
            {
                _subcategoryRepository = subcategoryRepository;
                _mapper = mapper;
                _subcategoryBusinessRules = subcategoryBusinessRules;
            }

            public async Task<UpdateSubcategoryDto> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
            {
                Subcategory mappedSubcategory = _mapper.Map<Subcategory>(request);
                Subcategory updatedSubcategory = await _subcategoryRepository.UpdateAsync(mappedSubcategory);
                UpdateSubcategoryDto updateSubcategory = _mapper.Map<UpdateSubcategoryDto>(updatedSubcategory);
                return updateSubcategory;
            }
        }
    }
}
