using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> ListAsync();
        Task<IEnumerable<Order>> ListAndRangeDateAsync(DateTime startAt, DateTime endAt);
        Task<IEnumerable<Order>> ListByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Order>> ListByEmployeeIdAndRangeDateAsync(int employeeId, DateTime startAt, DateTime endAt);
        Task<IEnumerable<Order>> ListByClientIdAsync(int clientId);
        Task<IEnumerable<Order>> ListByClientIdAndRangeDateAsync(int clientId, DateTime startAt, DateTime endAt);
        Task<IEnumerable<Order>> ListByPaymentMethodIdAsync(int paymentMethodId);
        Task<IEnumerable<Order>> ListByPaymentMethodIdAndRangeDateAsync(int paymentMethodId, DateTime startAt, DateTime endAt);
        Task<OrderResponse> GetByIdAsync(int id);
        Task<OrderResponse> SaveAsync(int clientId,Order order);
        Task<OrderResponse> UpdateAsync(int id, Order order);
        Task<OrderResponse> DeleteAsync(int id);
    }
}
