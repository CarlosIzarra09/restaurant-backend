﻿using DSonia.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Persistence.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> FindById(int id);
        Task<IEnumerable<Order>> ListAsync();
        Task<IEnumerable<Order>> ListAndRangeDateAsync(DateTime startAt, DateTime endAt);
        Task<IEnumerable<Order>> ListByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Order>> ListByEmployeeIdAndRangeDateAsync(int employeeId, DateTime startAt, DateTime endAt);
        Task<IEnumerable<Order>> ListByClientIdAsync(int clientId);
        Task<IEnumerable<Order>> ListByClientIdAndRangeDateAsync(int clientId, DateTime startAt, DateTime endAt);
        Task<IEnumerable<Order>> ListByPaymentMethodIdAsync(int paymentMethodId);
        Task<IEnumerable<Order>> ListByPaymentMethodIdAndRangeDateAsync(int paymentMethodId, DateTime startAt, DateTime endAt);

        Task AddAsync(Order order);
        void Update(Order order);
        void Remove(Order order);
    }
}
