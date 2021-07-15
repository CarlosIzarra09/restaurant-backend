using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services;
using DSonia.API.Extentions;
using DSonia.API.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Controllers
{
    [ApiController]
    [Route("/api/categories/{categoryId}/products", Name = "Products")]
    [Produces("application/json")]
    public class CategoryProductsController :ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public CategoryProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        


        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByCategoryIdAndProductIdAsync(int categoryId, int productId)
        {
            var result = await _productService.GetByCategoryIdAndProductIdAsync(categoryId, productId);
            if (!result.Success)
                return BadRequest(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Resource);

            return Ok(productResource);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(int categoryId, int productId)
        {
            var result = await _productService.DeleteAsync(categoryId, productId);
            if (!result.Success)
                return BadRequest(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Resource);
            return Ok(productResource);
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync(int categoryId, [FromBody] SaveProductResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.SaveAsync(categoryId, product);

            if (!result.Success)
                return BadRequest(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Resource);
            return Ok(productResource);
        }


        [HttpPut("{productId}")]
        public async Task<IActionResult> PutAsync(int categoryId, int productId, [FromBody] SaveProductResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.UpdateAsync(categoryId, productId, product);

            if (!result.Success)
                return BadRequest(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Resource);
            return Ok(productResource);
        }
    }
}
