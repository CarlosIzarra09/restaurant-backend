﻿using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services.Communication
{
    public class PayMoneylenderResponse : BaseResponse<PaymentMoneylender>
    {
        public PayMoneylenderResponse(PaymentMoneylender resource) : base(resource)
        {
        }

        public PayMoneylenderResponse(string message) : base(message)
        {
        }
    }
}
