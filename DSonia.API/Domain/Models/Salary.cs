using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public float mount { get; set; }
        public DateTime PayDate { get; set; }
        //relationship
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
