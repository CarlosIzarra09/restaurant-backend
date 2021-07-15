using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services.Communication
{
    public class SalaryResponse : BaseResponse<Salary>
    {
        public SalaryResponse(Salary resource) : base(resource)
        {
        }

        public SalaryResponse(string message) : base(message)
        {
        }
    }
}
