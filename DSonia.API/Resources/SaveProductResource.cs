using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class SaveProductResource
    {
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public int UnitInStock { get; set; }       
    }
}
