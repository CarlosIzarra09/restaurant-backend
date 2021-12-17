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
    public class OrderDetailRepository : BaseRepository, IOrderDetailRepository
    {
        
        public OrderDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
        }

        public async Task<OrderDetail> FindByOrderIdAndProductId(int orderId, int productId)
        {
            IEnumerable<OrderDetail> orderDetails = await _context.OrderDetails.Where(p => p.OrderId == orderId && p.ProductId == productId)
                .ToListAsync();
            return orderDetails.Any() ? orderDetails.First() : null;
        }

        public async Task<IEnumerable<OrderDetail>> ListByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                .Where(p => p.OrderId == orderId)
                .Include(p => p.Order)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> ListByProductIdAsync(int productId)
        {
            return await _context.OrderDetails
                .Where(p => p.ProductId == productId)
                .Include(p => p.Order)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public void Remove(OrderDetail orderDetail)
        {
            _context.OrderDetails.Remove(orderDetail);
        }
    }
}
