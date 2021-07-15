using DSonia.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Persistence.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethod>> ListAsync();
        Task<PaymentMethod> FindById(int id);
        Task AddAsync(PaymentMethod paymentMethod);
        void Update(PaymentMethod paymentMethod);
        void Remove(PaymentMethod paymentMethod);
    }
}
