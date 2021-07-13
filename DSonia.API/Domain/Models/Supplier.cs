using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public int PaySupplierId { get; set; }
        public IList<PaySupplier> PaySuppliers{ get; set; }
    }
}
