using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services.Communication
{
    public class PaySupplierResponse : BaseResponse<PaymentSupplier>
    {
        public PaySupplierResponse(PaymentSupplier resource) : base(resource)
        {
        }

        public PaySupplierResponse(string message) : base(message)
        {
        }
    }
}
