using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Domain.Entities
{
    public class Supplier:User
    {
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public int NumberOfProducts { get; set; }

        public Supplier()
        {
            
        }
    }
}
