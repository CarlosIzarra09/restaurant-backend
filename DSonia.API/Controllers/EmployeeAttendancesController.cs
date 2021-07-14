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
    [Authorize] //any users who will want access to this endpoint must be authenticaded
    [ApiController]
    [Route("/api/employees/{employeeId}/attendances")]
    [Produces("application/json")]
    public class EmployeeAttendancesController :ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _mapper;

        public EmployeeAttendancesController(IAttendanceService attendanceService, IMapper mapper)
        {
            _attendanceService = attendanceService;
            _mapper = mapper;
        }

       
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AttendanceResource>), 200)]
        public async Task<IEnumerable<AttendanceResource>> GetAllByEmployeeIdAndRange(int employeeId,[FromQuery] DateTime starAt,[FromQuery] DateTime endAt)
        {
            //string a ="{1/01/0001 00:00:00}";
            DateTime defaultInit = new DateTime(1, 1, 1, 0, 0, 0);
            DateTime defaultEnd = new DateTime(2040, 1, 1, 0, 0, 0);
            IEnumerable<Attendance> attendances;

            if(starAt.CompareTo(defaultInit)==0 && endAt.CompareTo(defaultInit)==0)
                attendances = await _attendanceService.ListByEmployeeIdAsync(employeeId);
            else
            {
                if(starAt.CompareTo(defaultInit) == 0)
                    attendances = await _attendanceService.ListByEmployeeIdAndRangeAsync(employeeId, defaultInit, endAt);
                else if(endAt.CompareTo(defaultInit) == 0)
                    attendances = await _attendanceService.ListByEmployeeIdAndRangeAsync(employeeId, starAt, defaultEnd);
                else 
                    attendances = await _attendanceService.ListByEmployeeIdAndRangeAsync(employeeId, starAt, endAt);
            }
            
            var resources = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceResource>>(attendances);

            return resources;
        }


        [HttpGet("{attendanceId}")]
        public async Task<IActionResult> GetByEmployeeIdAndAttendanceIdAsync(int employeeId, int attendanceId)
        {
            var result = await _attendanceService.GetByEmployeeIdAndAttendanceIdAsync(employeeId, attendanceId);
            if (!result.Success)
                return BadRequest(result.Message);

            var attendanceResource = _mapper.Map<Attendance, AttendanceResource>(result.Resource);

            return Ok(attendanceResource);
        }

        [HttpDelete("{attendanceId}")]
        public async Task<IActionResult> DeleteAsync(int employeeId, int attendanceId)
        {
            var result = await _attendanceService.DeleteAsync(employeeId,attendanceId);
            if (!result.Success)
                return BadRequest(result.Message);

            var attendanceResource = _mapper.Map<Attendance, AttendanceResource>(result.Resource);
            return Ok(attendanceResource);
        }

        
        [HttpPost]
        public async Task<IActionResult> PostAsync(int employeeId, [FromBody] SaveAttendanceResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var attendance = _mapper.Map<SaveAttendanceResource, Attendance>(resource);
            var result = await _attendanceService.SaveAsync(employeeId, attendance);

            if (!result.Success)
                return BadRequest(result.Message);

            var attendanceResource = _mapper.Map<Attendance, AttendanceResource>(result.Resource);
            return Ok(attendanceResource);
        }

        
        [HttpPut("{attendanceId}")]
        public async Task<IActionResult> PutAsync(int employeeId, int attendanceId, [FromBody] SaveAttendanceResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var attendance = _mapper.Map<SaveAttendanceResource, Attendance>(resource);
            var result = await _attendanceService.UpdateAsync(employeeId, attendanceId, attendance);

            if (!result.Success)
                return BadRequest(result.Message);

            var attendanceResource = _mapper.Map<Attendance, AttendanceResource>(result.Resource);
            return Ok(attendanceResource);
        }

    }
}
