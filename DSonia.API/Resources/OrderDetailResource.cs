using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class OrderDetailResource
    {
        public int Id { get; set; }
        public OrderResource Order { get; set; }
        public ProductResource Product { get; set; }
    }
}
