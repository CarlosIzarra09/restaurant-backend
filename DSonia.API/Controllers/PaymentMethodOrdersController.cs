using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services;
using DSonia.API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("/api/payment-methods/{paymentMethodId}/orders")]
    [Produces("application/json")]
    public class PaymentMethodOrdersController: ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public PaymentMethodOrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResource>), 200)]
        public async Task<IEnumerable<OrderResource>> GetAllByPaymentMethodIdAndRangeDate(int paymentMethodId, [FromQuery] DateTime starAt, [FromQuery] DateTime endAt)
        {
            //string a ="{1/01/0001 00:00:00}";
            DateTime defaultInit = new DateTime(1, 1, 1, 0, 0, 0);
            DateTime defaultEnd = new DateTime(2040, 1, 1, 0, 0, 0);
            IEnumerable<Order> orders;

            if (starAt.CompareTo(defaultInit) == 0 && endAt.CompareTo(defaultInit) == 0)
                orders = await _orderService.ListByPaymentMethodIdAsync(paymentMethodId);
            else
            {
                if (starAt.CompareTo(defaultInit) == 0)
                    orders = await _orderService.ListByPaymentMethodIdAndRangeDateAsync(paymentMethodId, defaultInit, endAt);
                else if (endAt.CompareTo(defaultInit) == 0)
                    orders = await _orderService.ListByPaymentMethodIdAndRangeDateAsync(paymentMethodId, starAt, defaultEnd);
                else
                    orders = await _orderService.ListByPaymentMethodIdAndRangeDateAsync(paymentMethodId, starAt, endAt);
            }

            var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);

            return resources;
        }
    }
}
