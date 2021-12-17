using DSonia.API.Domain.Models;
using DSonia.API.Domain.Persistence.Repositories;
using DSonia.API.Domain.Services;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Persistence.Repositories
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IUnitOfWork unitOfWork, 
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ProductResponse> DeleteAsync(int categoryId, int id)
        {
            var existingProduct = await _productRepository.FindByCategoryIdAndProductIdAsync(categoryId, id);
            if (existingProduct == null)
                return new ProductResponse("Product not found");
            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(existingProduct);
            }
            catch (Exception e)
            {
                return new ProductResponse("Has ocurred an error deleting the Product " + e.Message);
            }
        }

        public async Task<ProductResponse> GetByCategoryIdAndProductIdAsync(int categoryId, int productId)
        {
            var existingProduct = await _productRepository.FindByCategoryIdAndProductIdAsync(categoryId, productId);
            if (existingProduct == null)
                return new ProductResponse("Product not found");

            return new ProductResponse(existingProduct);
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<IEnumerable<Product>> ListByCategoryIdAsync(int categoryId)
        {
            return await _productRepository.ListByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> ListByOrderIdAsync(int orderId)
        {
            var orderDetails = await _orderDetailRepository.ListByOrderIdAsync(orderId);
            return orderDetails.Select(p => p.Product).ToList();
        }

        public async Task<ProductResponse> SaveAsync(int categoryId, Product product)
        {
            var existingCategory = await _categoryRepository.FindById(categoryId);

            if (existingCategory == null)
                return new ProductResponse("Category Id not found");

            try
            {
                product.CategoryId = categoryId;
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }
            catch (Exception e)
            {
                return new ProductResponse("Has ocurred an error saving the Product " + e.Message);
            }
        }

        public async Task<ProductResponse> UpdateAsync(int categoryId, int id, Product product)
        {
            var existingProduct = await _productRepository.FindByCategoryIdAndProductIdAsync(categoryId, id);
            if (existingProduct == null)
                return new ProductResponse("Product not found");

            try
            {
                existingProduct.Name = product.Name;
                existingProduct.UnitPrice = product.UnitPrice;     

                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(existingProduct);
            }
            catch (Exception e)
            {
                return new ProductResponse("Has ocurred an error updating the Product " + e.Message);
            }
        }
    }
}
