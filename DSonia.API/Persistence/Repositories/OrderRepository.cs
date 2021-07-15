using DSonia.API.Domain.Models;
using DSonia.API.Domain.Persistence.Contexts;
using DSonia.API.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Persistence.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<Order> FindById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> ListAndRangeDateAsync(DateTime startAt, DateTime endAt)
        {
            //Podriamos hacer que nos retorne todo, para pensar
            return await _context.Orders.Where(c => c.ShipDate.CompareTo(startAt) > 0 && c.ShipDate.CompareTo(endAt) < 0)
                .Include(c => c.Employee)
                .Include(c => c.Client)
                .Include(c=> c.PaymentMethod)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await _context.Orders
                .Include(c => c.Employee)
                .Include(c => c.Client)
                .Include(c => c.PaymentMethod)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListByClientIdAndRangeDateAsync(int clientId, DateTime startAt, DateTime endAt)
        {
            return await _context.Orders
                .Where(c => c.ShipDate.CompareTo(startAt) > 0 && c.ShipDate.CompareTo(endAt) < 0 && c.ClientId == clientId)
                .Include(c => c.Employee)
                .Include(c=>c.PaymentMethod)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListByClientIdAsync(int clientId)
        {
            return await _context.Orders
                .Where(c=> c.ClientId == clientId)
                .Include(c => c.Employee)
                .Include(c => c.PaymentMethod)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListByEmployeeIdAndRangeDateAsync(int employeeId, DateTime startAt, DateTime endAt)
        {
            return await _context.Orders
                .Where(c => c.ShipDate.CompareTo(startAt) > 0 && c.ShipDate.CompareTo(endAt) < 0 && c.EmployeeId == employeeId)
                .Include(c => c.Client)
                .Include(c => c.PaymentMethod)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListByEmployeeIdAsync(int employeeId)
        {
            return await _context.Orders
                .Where(c => c.EmployeeId == employeeId)
                .Include(c => c.Client)
                .Include(c => c.PaymentMethod)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListByPaymentMethodIdAndRangeDateAsync(int paymentMethodId, DateTime startAt, DateTime endAt)
        {
            return await _context.Orders
                .Where(c => c.ShipDate.CompareTo(startAt) > 0 && c.ShipDate.CompareTo(endAt) < 0 && c.PaymentMethodId == paymentMethodId)
                .Include(c => c.Client)
                .Include(c => c.Employee)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListByPaymentMethodIdAsync(int paymentMethodId)
        {
            return await _context.Orders
                .Where(c => c.PaymentMethodId == paymentMethodId)
                .Include(c => c.Client)
                .Include(c => c.Employee)
                .ToListAsync();
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
