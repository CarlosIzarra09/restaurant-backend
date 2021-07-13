using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public bool IsShipper { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<Salary> Salaries { get; set; }
        public IList<Attendance> Attendances { get; set; }
    }
}
