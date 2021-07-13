using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
