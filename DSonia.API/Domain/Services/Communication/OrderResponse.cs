using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services.Communication
{
    public class OrderResponse : BaseResponse<Order>
    {
        public OrderResponse(Order resource) : base(resource)
        {
        }

        public OrderResponse(string message) : base(message)
        {
        }
    }
}
