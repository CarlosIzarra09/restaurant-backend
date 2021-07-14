using DSonia.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Persistence.Repositories
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> ListAsync();
        Task<IEnumerable<Attendance>> ListByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Attendance>> ListByEmployeeIdAndRangeDateAsync(int employeeId, DateTime startAt, DateTime endAt);

        Task<Attendance> FindByEmployeeIdAndAttendanceIdAsync(int employeeId, int attendanceId);
        Task<Attendance> FindById(int id);
        Task AddAsync(Attendance attendance);
        void Update(Attendance attendance);
        void Remove(Attendance attendance);
    }
}

