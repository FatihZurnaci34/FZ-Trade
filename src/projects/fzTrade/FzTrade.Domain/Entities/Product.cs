using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Domain.Entities
{
    public class Product:Entity
    {
        public int SubcategoryId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set;}

        public virtual Subcategory Subcategory { get; set; }
        public Product()
        {
            
        }

        public Product(int id,int supplierId,int subcategoryId,string name,
                       string size,decimal price,string description,int stock ):this()
        {
            Id = id;
            SubcategoryId = subcategoryId;
            Name = name;
            Size = size;
            Price = price;
            Description = description;
            Stock = stock;
        }
    }
}
