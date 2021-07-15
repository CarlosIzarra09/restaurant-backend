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
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResource>), 200)]
        public async Task<IEnumerable<OrderResource>> GetAllByRangeDate([FromQuery] DateTime starAt, [FromQuery] DateTime endAt)
        {
            //string a ="{1/01/0001 00:00:00}";
            DateTime defaultInit = new DateTime(1, 1, 1, 0, 0, 0);
            DateTime defaultEnd = new DateTime(2040, 1, 1, 0, 0, 0);
            IEnumerable<Order> orders;

            if (starAt.CompareTo(defaultInit) == 0 && endAt.CompareTo(defaultInit) == 0)
                orders = await _orderService.ListAsync();
            else
            {
                if (starAt.CompareTo(defaultInit) == 0)
                    orders = await _orderService.ListAndRangeDateAsync(defaultInit, endAt);
                else if (endAt.CompareTo(defaultInit) == 0)
                    orders = await _orderService.ListAndRangeDateAsync(starAt, defaultEnd);
                else
                    orders = await _orderService.ListAndRangeDateAsync(starAt, endAt);
            }

            var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);

            return resources;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetByIdAsync(int orderId)
        {
            var result = await _orderService.GetByIdAsync(orderId);
            if (!result.Success)
                return BadRequest(result.Message);


            var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
            return Ok(orderResource);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteAsync(int orderId)
        {
            var result = await _orderService.DeleteAsync(orderId);
            if (!result.Success)
                return BadRequest(result.Message);

            var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
            return Ok(orderResource);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> PutAsync(int orderId, [FromBody] SaveOrderResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var order = _mapper.Map<SaveOrderResource, Order>(resource);
            var result = await _orderService.UpdateAsync(orderId, order);

            if (!result.Success)
                return BadRequest(result.Message);

            var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
            return Ok(orderResource);
        }
    }
}
