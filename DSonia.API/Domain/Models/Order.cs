using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime RequiredTime { get; set; }
        public DateTime ShipTime { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipAddress { get; set; }
        public float ShipPrice { get; set; }
        public float Debt { get; set; }
        public string Description { get; set; }

        //Relationships
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public IList<OrderDetail> OrderDetails { get; set; }
    }
}
