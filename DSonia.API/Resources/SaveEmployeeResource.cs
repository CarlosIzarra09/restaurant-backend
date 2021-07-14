using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class SaveEmployeeResource
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public int Phone { get; set; }
        [Required]
        public bool IsShipper { get; set; }
    }
}
