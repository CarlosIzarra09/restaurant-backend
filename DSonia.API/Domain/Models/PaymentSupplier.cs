using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class PaymentSupplier
    {
        public int Id { get; set; }
        public DateTime PayDate { get; set; }
        public string Comment { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
