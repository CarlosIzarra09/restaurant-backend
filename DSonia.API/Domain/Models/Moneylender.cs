using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class Moneylender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime DebtTotal { get; set; }
        public IList<PaymentMoneylender> PaymentMoneylenders { get; set; }
    }
}
