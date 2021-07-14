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
    public class EmployeeRepository : BaseRepository,IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task<Employee> FindById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public void Remove(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }
    }
}
