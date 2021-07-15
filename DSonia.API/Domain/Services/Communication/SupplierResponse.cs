using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services.Communication
{
    public class SupplierResponse : BaseResponse<Supplier>
    {
        public SupplierResponse(Supplier resource) : base(resource)
        {
        }

        public SupplierResponse(string message) : base(message)
        {
        }
    }
}
