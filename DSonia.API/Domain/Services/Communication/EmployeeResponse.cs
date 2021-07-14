using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services.Communication
{
    public class EmployeeResponse : BaseResponse<Employee>
    {
        public EmployeeResponse(Employee resource) : base(resource)
        {
        }

        public EmployeeResponse(string message) : base(message)
        {
        }
    }
}
