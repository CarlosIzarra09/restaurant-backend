using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class SaveOrderResource
    {
        [Required]
        public DateTime RequiredTime { get; set; }
        [Required]
        public DateTime ShipTime { get; set; }
        [Required]
        public DateTime ShipDate { get; set; }
        [Required]
        public string ShipAddress { get; set; }
        [Required]
        public float ShipPrice { get; set; }
        [Required]
        public float Debt { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int PaymentMethodId { get; set; }
    
    }
}
