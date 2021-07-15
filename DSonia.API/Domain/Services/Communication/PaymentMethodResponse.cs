using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services.Communication
{
    public class PaymentMethodResponse : BaseResponse<PaymentMethod>
    {
        public PaymentMethodResponse(string message) : base(message)
        {
        }

        public PaymentMethodResponse(PaymentMethod resource) : base(resource)
        {
        }
    }
}
