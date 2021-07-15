using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class OrderResource
    {
        public int Id { get; set; }
        public DateTime RequiredTime { get; set; }
        public DateTime ShipTime { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipAddress { get; set; }
        public float ShipPrice { get; set; }
        public float Debt { get; set; }
        public string Description { get; set; }

        public EmployeeResource Employee { get; set; }
        public ClientResource Client { get; set; }
        public PaymentMethodResource PaymentMethod { get; set; }
    }
}
