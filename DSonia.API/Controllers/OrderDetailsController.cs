using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services;
using DSonia.API.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Controllers
{
    [ApiController]
    [Route("/api/orders/{orderId}/products", Name = "OrderDetails")]
    [Produces("application/json")]
    public class OrderDetailsController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IMapper mapper, IProductService productService, IOrderDetailService orderDetailService)
        {
            _mapper = mapper;
            _productService = productService;
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResource>> GetProductsByOrderIdAsync(int orderId)
        {
            var products = await _productService.ListByOrderIdAsync(orderId);
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

            return resources;
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(int orderId, int productId)
        {
            var result = await _orderDetailService.UnassignOrderDetailAsync(orderId, productId);
            if (!result.Success)
                return BadRequest(result.Message);

            var orderDetailResource = _mapper.Map<OrderDetail, OrderDetailResource>(result.Resource);
            return Ok(orderDetailResource);
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> PostAsync(int orderId, int productId)
        {
            var result = await _orderDetailService.AssignOrderDetailAsync(orderId, productId);
            if (!result.Success)
                return BadRequest(result.Message);

            var orderDetailResource = _mapper.Map<OrderDetail, OrderDetailResource>(result.Resource);
            return Ok(orderDetailResource);
        }


    }
}
