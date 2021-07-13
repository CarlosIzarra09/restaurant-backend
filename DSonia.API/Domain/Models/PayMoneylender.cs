using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class PayMoneylender
    {
        public int Id { get; set; }
        public float Amortize { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime PayTime { get; set; }
        public Moneylender Moneylender { get; set; }
    }
}
