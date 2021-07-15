using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethod>> ListAsync();
        Task<PaymentMethodResponse> GetByIdAsync(int id);
        Task<PaymentMethodResponse> SaveAsync(PaymentMethod payment);
        Task<PaymentMethodResponse> UpdateAsync(int id, PaymentMethod payment);
        Task<PaymentMethodResponse> DeleteAsync(int id);
    }
}
