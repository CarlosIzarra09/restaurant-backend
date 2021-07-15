using DSonia.API.Domain.Models;
using DSonia.API.Domain.Persistence.Repositories;
using DSonia.API.Domain.Services;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(IUnitOfWork unitOfWork, IPaymentMethodRepository paymentRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentMethodRepository = paymentRepository;
        }

        public async Task<PaymentMethodResponse> DeleteAsync(int id)
        {
            var existingPaymentMethod = await _paymentMethodRepository.FindById(id);
            if (existingPaymentMethod == null)
                return new PaymentMethodResponse("PaymentMethod not found");
            try
            {
                _paymentMethodRepository.Remove(existingPaymentMethod);
                await _unitOfWork.CompleteAsync();
                return new PaymentMethodResponse(existingPaymentMethod);
            }
            catch (Exception e)
            {
                return new PaymentMethodResponse("Has ocurred an error deleting the PaymentMethod " + e.Message);
            }
        }

        public async Task<PaymentMethodResponse> GetByIdAsync(int id)
        {
            var existingPaymentMethod = await _paymentMethodRepository.FindById(id);
            if (existingPaymentMethod == null)
                return new PaymentMethodResponse("PaymentMethod not found");

            return new PaymentMethodResponse(existingPaymentMethod);
        }

        public async Task<IEnumerable<PaymentMethod>> ListAsync()
        {
            return await _paymentMethodRepository.ListAsync();
        }

        public async Task<PaymentMethodResponse> SaveAsync(PaymentMethod payment)
        {
            try
            {
                await _paymentMethodRepository.AddAsync(payment);
                await _unitOfWork.CompleteAsync();

                return new PaymentMethodResponse(payment);
            }
            catch (Exception e)
            {
                return new PaymentMethodResponse($"An error ocurred while saving the PaymentMethod: {e.Message}");
            }
        }

        public async Task<PaymentMethodResponse> UpdateAsync(int id, PaymentMethod payment)
        {
            var existingPaymentMethod = await _paymentMethodRepository.FindById(id);
            if (existingPaymentMethod == null)
                return new PaymentMethodResponse("PaymentMethod not found");

            existingPaymentMethod.Name = payment.Name;
            
            try
            {
                _paymentMethodRepository.Update(existingPaymentMethod);
                await _unitOfWork.CompleteAsync();

                return new PaymentMethodResponse(existingPaymentMethod);
            }
            catch (Exception ex)
            {
                return new PaymentMethodResponse($"An error ocurred while updating the PaymentMethod: {ex.Message}");
            }
        }
    }
}
