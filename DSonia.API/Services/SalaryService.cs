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
    public class SalaryService : ISalaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISalaryRepository _salaryRepository;

        public SalaryService(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository, ISalaryRepository salaryRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _salaryRepository = salaryRepository;
        }

        public async Task<SalaryResponse> DeleteAsync(int employeeId, int id)
        {
            var existingSalary = await _salaryRepository.FindByEmployeeIdAndSalaryIdAsync(employeeId, id);
            if (existingSalary == null)
                return new SalaryResponse("Salary not found");
            try
            {
                _salaryRepository.Remove(existingSalary);
                await _unitOfWork.CompleteAsync();
                return new SalaryResponse(existingSalary);
            }
            catch (Exception e)
            {
                return new SalaryResponse("Has ocurred an error deleting the Salary " + e.Message);
            }
        }

        public async Task<SalaryResponse> GetByEmployeeIdAndSalaryIdAsync(int employeeId, int salaryId)
        {
            var existingSalary = await _salaryRepository.FindByEmployeeIdAndSalaryIdAsync(employeeId, salaryId);
            if (existingSalary == null)
                return new SalaryResponse("Salary not found");

            return new SalaryResponse(existingSalary);
        }

        public async Task<IEnumerable<Salary>> ListAsync()
        {
            return await _salaryRepository.ListAsync();
        }

        public async Task<IEnumerable<Salary>> ListByEmployeeIdAndRangeAsync(int employeeId, DateTime startAt, DateTime endAt)
        {
            return await _salaryRepository.ListByEmployeeIdAndRangeDateAsync(employeeId, startAt, endAt);
        }

        public async Task<IEnumerable<Salary>> ListByEmployeeIdAsync(int employeeId)
        {
            return await _salaryRepository.ListByEmployeeIdAsync(employeeId);
        }

        public async Task<SalaryResponse> SaveAsync(int employeeId, Salary salary)
        {
            var existingEmployee = await _employeeRepository.FindById(employeeId);

            if (existingEmployee == null)
                return new SalaryResponse("Employee Id not found");

            try
            {
                salary.EmployeeId = employeeId;
                await _salaryRepository.AddAsync(salary);
                await _unitOfWork.CompleteAsync();
                return new SalaryResponse(salary);
            }
            catch (Exception e)
            {
                return new SalaryResponse("Has ocurred an error saving the Salary " + e.Message);
            }
        }

        public async Task<SalaryResponse> UpdateAsync(int employeeId, int id, Salary salary)
        {
            var existingSalary = await _salaryRepository.FindByEmployeeIdAndSalaryIdAsync(employeeId, id);
            if (existingSalary == null)
                return new SalaryResponse("Salary not found");

            try
            {
                existingSalary.PayDate = salary.PayDate;
                existingSalary.Mount = salary.Mount;
                _salaryRepository.Update(existingSalary);
                await _unitOfWork.CompleteAsync();
                return new SalaryResponse(existingSalary);
            }
            catch (Exception e)
            {
                return new SalaryResponse("Has ocurred an error updating the Salary " + e.Message);
            }
        }
    }
}
