using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services.Communication
{
    public class AttendanceResponse : BaseResponse<Attendance>
    {
        public AttendanceResponse(Attendance resource) : base(resource)
        {
        }

        public AttendanceResponse(string message) : base(message)
        {
        }
    }
}
