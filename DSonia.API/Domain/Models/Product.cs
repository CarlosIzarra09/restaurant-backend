using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; }
    }
}
