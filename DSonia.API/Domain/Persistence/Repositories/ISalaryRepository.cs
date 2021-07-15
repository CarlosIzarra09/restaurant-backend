using DSonia.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Persistence.Repositories
{
    public interface ISalaryRepository
    {
        Task<IEnumerable<Salary>> ListAsync();
        Task<IEnumerable<Salary>> ListByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Salary>> ListByEmployeeIdAndRangeDateAsync(int employeeId, DateTime startAt, DateTime endAt);

        Task<Salary> FindByEmployeeIdAndSalaryIdAsync(int employeeId, int salaryId);
        Task<Salary> FindById(int id);
        Task AddAsync(Salary salary);
        void Update(Salary salary);
        void Remove(Salary salary);
    }
}
