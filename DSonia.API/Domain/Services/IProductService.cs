using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<IEnumerable<Product>> ListByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> ListByOrderIdAsync(int orderId);

        Task<ProductResponse> GetByCategoryIdAndProductIdAsync(int categoryId, int productId);
        Task<ProductResponse> SaveAsync(int categoryId, Product product);
        Task<ProductResponse> UpdateAsync(int categoryId, int id, Product product);
        Task<ProductResponse> DeleteAsync(int categoryId, int id);
    }
}
