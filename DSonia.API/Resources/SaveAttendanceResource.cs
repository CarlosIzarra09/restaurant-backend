using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class SaveAttendanceResource
    {
        [Required]
        public DateTime EntryTime { get; set; }
        [Required]
        public DateTime OutTime { get; set; }
        [Required]
        public DateTime AttendanceDate { get; set; }        
    }
}
