using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Domain.Entities
{
    public class Customer:User
    {
        public bool Gender { get; set; }
        public decimal Balance { get; set; }
        public string? Address { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Customer()
        {
            
        }
    }
}
