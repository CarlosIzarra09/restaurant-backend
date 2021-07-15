using DSonia.API.Domain.Models;
using DSonia.API.Domain.Persistence.Repositories;
using DSonia.API.Domain.Services;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindById(id);
            if (existingCategory == null)
                return new CategoryResponse("Category not found");
            try
            {
                _categoryRepository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();
                return new CategoryResponse(existingCategory);
            }
            catch (Exception e)
            {
                return new CategoryResponse("Has ocurred an error deleting the Category " + e.Message);
            }
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindById(id);
            if (existingCategory == null)
                return new CategoryResponse("Category not found");

            return new CategoryResponse(existingCategory);
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CompleteAsync();

                return new CategoryResponse(category);
            }
            catch (Exception e)
            {
                return new CategoryResponse($"An error ocurred while saving the Category: {e.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.FindById(id);
            if (existingCategory == null)
                return new CategoryResponse("Category not found");

            existingCategory.Name = category.Name;
           
            try
            {
                _categoryRepository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"An error ocurred while updating the Category: {ex.Message}");
            }
        }
    }
}
