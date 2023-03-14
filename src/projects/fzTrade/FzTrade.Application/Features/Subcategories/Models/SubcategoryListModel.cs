using Core.Persistence.Paging;
using FzTrade.Application.Features.Subcategories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Subcategories.Models
{
    public class SubcategoryListModel:BasePageableModel
    {
        public List<SubcategoryListDto> Items { get; set; }
    }
}
