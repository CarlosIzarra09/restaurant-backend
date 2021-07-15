using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services
{
    public interface ISalaryService
    {
        Task<IEnumerable<Salary>> ListAsync();
        Task<IEnumerable<Salary>> ListByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Salary>> ListByEmployeeIdAndRangeAsync(int employeeId, DateTime startAt, DateTime endAt);

        Task<SalaryResponse> GetByEmployeeIdAndSalaryIdAsync(int employeeId, int salaryId);
        Task<SalaryResponse> SaveAsync(int employeeId, Salary salary);
        Task<SalaryResponse> UpdateAsync(int employeeId, int id, Salary salary);
        Task<SalaryResponse> DeleteAsync(int employeeId, int id);
    }
}
