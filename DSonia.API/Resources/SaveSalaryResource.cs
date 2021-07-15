using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class SaveSalaryResource
    {
        [Required]
        public float Mount { get; set; }
        [Required]
        public DateTime PayDate { get; set; }
        //relationship
    }
}
