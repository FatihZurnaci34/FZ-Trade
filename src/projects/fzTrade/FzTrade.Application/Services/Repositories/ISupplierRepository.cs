using Core.Persistence.Repositories;
using FzTrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Services.Repositories
{
    public interface ISupplierRepository:IAsyncRepository<Supplier>,IRepository<Supplier>
    {
    }
}
