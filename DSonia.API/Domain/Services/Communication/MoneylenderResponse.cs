using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services.Communication
{
    public class MoneylenderResponse : BaseResponse<Moneylender>
    {
        public MoneylenderResponse(string message) : base(message)
        {
        }

        public MoneylenderResponse(Moneylender resource) : base(resource)
        {
        }
    }
}
