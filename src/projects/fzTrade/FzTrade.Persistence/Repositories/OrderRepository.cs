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
    public class OrderRepository : EfRepositoryBase<Order, BaseDbContext>, IOrderRepository
    {
        public OrderRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
