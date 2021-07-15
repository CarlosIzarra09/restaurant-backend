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
    [Route("/api/payment-methods")]
    [Produces("application/json")]
    public class PaymentMethodsController: ControllerBase
    {
        private readonly IPaymentMethodService _paymentService;
        private readonly IMapper _mapper;

        public PaymentMethodsController(IPaymentMethodService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentMethodResource>> GetAllAsync()
        {
            var paymentMethods = await _paymentService.ListAsync();
            var resources = _mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<PaymentMethodResource>>(paymentMethods);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _paymentService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentMethodResource = _mapper.Map<PaymentMethod, PaymentMethodResource>(result.Resource);
            return Ok(paymentMethodResource);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePaymentMethodResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var paymentMethod = _mapper.Map<SavePaymentMethodResource, PaymentMethod>(resource);
            var result = await _paymentService.SaveAsync(paymentMethod);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentMethodResource = _mapper.Map<PaymentMethod, PaymentMethodResource>(result.Resource);

            return Ok(paymentMethodResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePaymentMethodResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var paymentMethod = _mapper.Map<SavePaymentMethodResource, PaymentMethod>(resource);
            var result = await _paymentService.UpdateAsync(id, paymentMethod);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentMethodResource = _mapper.Map<PaymentMethod, PaymentMethodResource>(result.Resource);

            return Ok(paymentMethodResource);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _paymentService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentMethodResource = _mapper.Map<PaymentMethod, PaymentMethodResource>(result.Resource);

            return Ok(paymentMethodResource);
        }
    }
}
