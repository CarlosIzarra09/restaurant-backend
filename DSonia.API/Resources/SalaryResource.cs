using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class SalaryResource
    {
        public int Id { get; set; }
        public float Mount { get; set; }
        public DateTime PayDate { get; set; }
        //relationship
        public EmployeeResource Employee { get; set; }
    }
}
