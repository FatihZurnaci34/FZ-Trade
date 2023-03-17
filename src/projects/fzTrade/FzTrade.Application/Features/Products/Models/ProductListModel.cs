using Core.Persistence.Paging;
using FzTrade.Application.Features.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Products.Models
{
    public class ProductListModel:BasePageableModel
    {
        public List<ProductGetListDto> Items { get; set; }
    }
}
