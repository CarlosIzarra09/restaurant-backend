using DSonia.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<IEnumerable<Product>> ListByCategoryIdAsync(int categoryId);
        Task<Product> FindByCategoryIdAndProductIdAsync(int categoryId, int productId);
        Task<Product> FindById(int id);
        Task AddAsync(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}
