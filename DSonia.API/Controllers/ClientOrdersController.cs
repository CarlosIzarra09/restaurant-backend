using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services;
using DSonia.API.Extentions;
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
    [Route("/api/clients/{clientId}/orders")]
    [Produces("application/json")]
    public class ClientOrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public ClientOrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResource>), 200)]
        public async Task<IEnumerable<OrderResource>> GetAllByclientIdAndRangeDate(int clientId, [FromQuery] DateTime starAt, [FromQuery] DateTime endAt)
        {
            //string a ="{1/01/0001 00:00:00}";
            DateTime defaultInit = new DateTime(1, 1, 1, 0, 0, 0);
            DateTime defaultEnd = new DateTime(2040, 1, 1, 0, 0, 0);
            IEnumerable<Order> orders;

            if (starAt.CompareTo(defaultInit) == 0 && endAt.CompareTo(defaultInit) == 0)
                orders = await _orderService.ListByClientIdAsync(clientId);
            else
            {
                if (starAt.CompareTo(defaultInit) == 0)
                    orders = await _orderService.ListByClientIdAndRangeDateAsync(clientId, defaultInit, endAt);
                else if (endAt.CompareTo(defaultInit) == 0)
                    orders = await _orderService.ListByClientIdAndRangeDateAsync(clientId, starAt, defaultEnd);
                else
                    orders = await _orderService.ListByClientIdAndRangeDateAsync(clientId, starAt, endAt);
            }

            var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);

            return resources;
        }

        


        [HttpPost]
        public async Task<IActionResult> PostAsync(int clientId, [FromBody] SaveOrderResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var order = _mapper.Map<SaveOrderResource, Order>(resource);
            var result = await _orderService.SaveAsync(clientId, order);

            if (!result.Success)
                return BadRequest(result.Message);

            var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
            return Ok(orderResource);
        }


        
    }
}
