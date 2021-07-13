using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime AttendanceDate { get; set; }
        //relationship
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
