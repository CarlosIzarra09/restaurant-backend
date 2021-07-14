using DSonia.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Persistence.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> ListAsync();
        Task<Employee> FindById(int id);
        Task AddAsync(Employee employee);
        void Update(Employee employee);
        void Remove(Employee employee);
    }
}
