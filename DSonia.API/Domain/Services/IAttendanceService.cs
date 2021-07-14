using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> ListAsync();
        Task<IEnumerable<Attendance>> ListByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Attendance>> ListByEmployeeIdAndRangeAsync(int employeeId, DateTime startAt, DateTime endAt);

        Task<AttendanceResponse> GetByEmployeeIdAndAttendanceIdAsync(int employeeId, int attendanceId);
        Task<AttendanceResponse> SaveAsync(int employeeId, Attendance attendance);
        Task<AttendanceResponse> UpdateAsync(int employeeId, int id, Attendance attendance);
        Task<AttendanceResponse> DeleteAsync(int employeeId, int id);
    }
}
