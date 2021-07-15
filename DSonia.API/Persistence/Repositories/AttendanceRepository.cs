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
    public class AttendanceRepository : BaseRepository, IAttendanceRepository
    {
        public AttendanceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
        }

        public async Task<Attendance> FindByEmployeeIdAndAttendanceIdAsync(int employeeId, int attendanceId)
        {
            IEnumerable<Attendance> attendances = await _context.Attendances.Where(p => p.EmployeeId == employeeId && p.Id == attendanceId)
                .ToListAsync();
            return !attendances.Any() ? attendances.First():null;
        }

        public async Task<Attendance> FindById(int id)
        {
            return await _context.Attendances.FindAsync(id);
        }

        public async Task<IEnumerable<Attendance>> ListAsync()
        {
            return await _context.Attendances.ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> ListByEmployeeIdAndRangeDateAsync(int employeeId, DateTime startAt, DateTime endAt)
        {
            return await _context.Attendances
                .Where(c => c.AttendanceDate.CompareTo(startAt) > 0 && c.AttendanceDate.CompareTo(endAt) < 0 && c.EmployeeId == employeeId)
                .Include(c => c.Employee)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> ListByEmployeeIdAsync(int employeeId)
        {
            return await _context.Attendances
                .Where(c => c.EmployeeId == employeeId)
                .Include(c => c.Employee)
                .ToListAsync();
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }

        public void Update(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
        }
    }
}
