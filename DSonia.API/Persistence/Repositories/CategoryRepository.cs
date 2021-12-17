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
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category> FindById(int id)
        {
            var category = await _context.Categories.Where(c => c.Id == id).Include(c => c.Products).ToListAsync();
            return category.Any() ? category.First() : null;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
