using DSonia.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class AttendanceResource
    {
        public int Id { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime AttendanceDate { get; set; }
        public EmployeeResource Employee { get; set; }
    }
}
