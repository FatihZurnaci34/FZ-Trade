using Core.Persistence.Repositories;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using FzTrade.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Persistence.Repositories
{
    public class SubcategoryRepository : EfRepositoryBase<Subcategory, BaseDbContext>, ISubcategory
    {
        public SubcategoryRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
