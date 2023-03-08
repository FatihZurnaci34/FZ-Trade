using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Domain.Entities
{
    public class Subcategory:Entity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<Product> Products { get; set; }
        public Subcategory()
        {
            
        }

        public Subcategory(int id,int categoryId,string name):this()
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
        }

    }
}
