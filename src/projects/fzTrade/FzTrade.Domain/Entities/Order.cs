using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Domain.Entities
{
    public class Order:Entity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        public ICollection<Product> Products { get; set; }
        public virtual Customer Customer { get; set; }

        public Order()
        {
            
        }
        public Order(int id,int productId,int customerId,DateTime orderDate):this()
        {
            Id = id;
            ProductId = productId;
            CustomerId = customerId;
            OrderDate = orderDate;
        }
    }
}
