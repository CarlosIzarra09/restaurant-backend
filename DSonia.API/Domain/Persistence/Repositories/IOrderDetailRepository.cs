using DSonia.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Persistence.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> ListByProductIdAsync(int productId);
        Task<IEnumerable<OrderDetail>> ListByOrderIdAsync(int orderId);
        Task<OrderDetail> FindByOrderIdAndProductId(int orderId, int productId);
        Task AddAsync(OrderDetail orderDetail);
        void Remove(OrderDetail orderDetail);
    }
}
