using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services;
using DSonia.API.Extentions;
using DSonia.API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Controllers
{
    //[Authorize] 
    [ApiController]
    [Route("/api/employees/{employeeId}/salaries", Name = "Salaries")]
    [Produces("application/json")]
    public class EmployeeSalariesController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        private readonly IMapper _mapper;

        public EmployeeSalariesController(ISalaryService salaryService, IMapper mapper)
        {
            _salaryService = salaryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalaryResource>), 200)]
        public async Task<IEnumerable<SalaryResource>> GetAllByEmployeeIdAndRange(int employeeId, [FromQuery] DateTime starAt, [FromQuery] DateTime endAt)
        {
            //string a ="{1/01/0001 00:00:00}";
            DateTime defaultInit = new DateTime(1, 1, 1, 0, 0, 0);
            DateTime defaultEnd = new DateTime(2040, 1, 1, 0, 0, 0);
            IEnumerable<Salary> salaries;

            if (starAt.CompareTo(defaultInit) == 0 && endAt.CompareTo(defaultInit) == 0)
                salaries = await _salaryService.ListByEmployeeIdAsync(employeeId);
            else
            {
                if (starAt.CompareTo(defaultInit) == 0)
                    salaries = await _salaryService.ListByEmployeeIdAndRangeAsync(employeeId, defaultInit, endAt);
                else if (endAt.CompareTo(defaultInit) == 0)
                    salaries = await _salaryService.ListByEmployeeIdAndRangeAsync(employeeId, starAt, defaultEnd);
                else
                    salaries = await _salaryService.ListByEmployeeIdAndRangeAsync(employeeId, starAt, endAt);
            }

            var resources = _mapper.Map<IEnumerable<Salary>, IEnumerable<SalaryResource>>(salaries);

            return resources;
        }


        [HttpGet("{salaryId}")]
        public async Task<IActionResult> GetByEmployeeIdAndsalaryIdAsync(int employeeId, int salaryId)
        {
            var result = await _salaryService.GetByEmployeeIdAndSalaryIdAsync(employeeId, salaryId);
            if (!result.Success)
                return BadRequest(result.Message);

            var SalaryResource = _mapper.Map<Salary, SalaryResource>(result.Resource);

            return Ok(SalaryResource);
        }

        [HttpDelete("{salaryId}")]
        public async Task<IActionResult> DeleteAsync(int employeeId, int salaryId)
        {
            var result = await _salaryService.DeleteAsync(employeeId, salaryId);
            if (!result.Success)
                return BadRequest(result.Message);

            var SalaryResource = _mapper.Map<Salary, SalaryResource>(result.Resource);
            return Ok(SalaryResource);
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync(int employeeId, [FromBody] SaveSalaryResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var Salary = _mapper.Map<SaveSalaryResource, Salary>(resource);
            var result = await _salaryService.SaveAsync(employeeId, Salary);

            if (!result.Success)
                return BadRequest(result.Message);

            var SalaryResource = _mapper.Map<Salary, SalaryResource>(result.Resource);
            return Ok(SalaryResource);
        }


        [HttpPut("{salaryId}")]
        public async Task<IActionResult> PutAsync(int employeeId, int salaryId, [FromBody] SaveSalaryResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var Salary = _mapper.Map<SaveSalaryResource, Salary>(resource);
            var result = await _salaryService.UpdateAsync(employeeId, salaryId, Salary);

            if (!result.Success)
                return BadRequest(result.Message);

            var SalaryResource = _mapper.Map<Salary, SalaryResource>(result.Resource);
            return Ok(SalaryResource);
        }
    }
}
