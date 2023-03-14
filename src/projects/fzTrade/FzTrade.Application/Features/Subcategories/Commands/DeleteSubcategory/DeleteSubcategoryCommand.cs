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

namespace FzTrade.Application.Features.Subcategories.Commands.DeleteSubcategory
{
    public class DeleteSubcategoryCommand:IRequest<DeleteSubcategoryDto>
    {
        public int Id { get; set; }
        public class DeleteSubcategoryCommandHandler : IRequestHandler<DeleteSubcategoryCommand, DeleteSubcategoryDto>
        {
            private readonly IMapper _mapper;
            private readonly ISubcategorRepository _subcategorRepository;
            private readonly SubcategoryBusinessRules _subcategoryBusinessRules;

            public DeleteSubcategoryCommandHandler(IMapper mapper, ISubcategorRepository subcategorRepository, SubcategoryBusinessRules subcategoryBusinessRules)
            {
                _mapper = mapper;
                _subcategorRepository = subcategorRepository;
                _subcategoryBusinessRules = subcategoryBusinessRules;
            }

            public async Task<DeleteSubcategoryDto> Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
            {
                Subcategory mappedSubcategory = _mapper.Map<Subcategory>(request);
                Subcategory createdSubcategory = await _subcategorRepository.DeleteAsync(mappedSubcategory);
                DeleteSubcategoryDto createSubcategory = _mapper.Map<DeleteSubcategoryDto>(createdSubcategory);
                return createSubcategory;
            }
        }
    }
}
