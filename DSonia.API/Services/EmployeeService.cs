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
    public class EmployeeService:IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeResponse> DeleteAsync(int id)
        {
            var existingEmployee = await _employeeRepository.FindById(id);
            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found");
            try
            {
                _employeeRepository.Remove(existingEmployee);
                await _unitOfWork.CompleteAsync();
                return new EmployeeResponse(existingEmployee);
            }
            catch (Exception e)
            {
                return new EmployeeResponse("Has ocurred an error deleting the employee " + e.Message);
            }
        }

        public async Task<EmployeeResponse> GetByIdAsync(int id)
        {
            var existingEmployee = await _employeeRepository.FindById(id);
            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found");

            return new EmployeeResponse(existingEmployee);
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _employeeRepository.ListAsync();
        }

        public async Task<EmployeeResponse> SaveAsync(Employee employee)
        {
            try
            {
                await _employeeRepository.AddAsync(employee);
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse(employee);
            }
            catch (Exception e)
            {
                return new EmployeeResponse($"An error ocurred while saving the employee: {e.Message}");
            }
        }

        public async Task<EmployeeResponse> UpdateAsync(int id, Employee employee)
        {
            var existingEmployee = await _employeeRepository.FindById(id);
            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found");

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.IsShipper = employee.IsShipper;
            try
            {
                _employeeRepository.Update(existingEmployee);
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse(existingEmployee);
            }
            catch (Exception ex)
            {
                return new EmployeeResponse($"An error ocurred while updating the employee: {ex.Message}");
            }
        }
    }
}
