using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Resources
{
    public class SaveClientResource
    {   
        [Required]
        public string Name { get; set; }
        public int Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
