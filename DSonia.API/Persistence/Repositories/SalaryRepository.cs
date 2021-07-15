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
    public class SalaryRepository : BaseRepository, ISalaryRepository
    {
        public SalaryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Salary salary)
        {
            await _context.Salaries.AddAsync(salary);
        }

        public async Task<Salary> FindByEmployeeIdAndSalaryIdAsync(int employeeId, int salaryId)
        {
            IEnumerable<Salary> salaries = await _context.Salaries.Where(p => p.EmployeeId == employeeId && p.Id == salaryId)
                .ToListAsync();
            return salaries.Count() == 0 ? null : salaries.First();
        }

        public async Task<Salary> FindById(int id)
        {
            return await _context.Salaries.FindAsync(id);
        }

        public async Task<IEnumerable<Salary>> ListAsync()
        {
            return await _context.Salaries.ToListAsync();
        }

        public async Task<IEnumerable<Salary>> ListByEmployeeIdAndRangeDateAsync(int employeeId, DateTime startAt, DateTime endAt)
        {
            return await _context.Salaries
                .Where(c => c.PayDate.CompareTo(startAt) > 0 && c.PayDate.CompareTo(endAt) < 0)
                .Include(c => c.Employee)
                .ToListAsync();
        }

        public async Task<IEnumerable<Salary>> ListByEmployeeIdAsync(int employeeId)
        {
            return await _context.Salaries
               .Where(c => c.EmployeeId == employeeId)
               .Include(c => c.Employee)
               .ToListAsync();
        }

        public void Remove(Salary salary)
        {
            _context.Salaries.Remove(salary);
        }

        public void Update(Salary salary)
        {
            _context.Salaries.Update(salary);
        }
    }
}
