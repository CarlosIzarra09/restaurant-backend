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
    public class AttendanceService :IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IUnitOfWork unitOfWork, IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<AttendanceResponse> DeleteAsync(int employeeId, int id)
        {
            var existingAttendance = await _attendanceRepository.FindByEmployeeIdAndAttendanceIdAsync(employeeId, id);
            if (existingAttendance == null)
                return new AttendanceResponse("Attendance not found");
            try
            {
                _attendanceRepository.Remove(existingAttendance);
                await _unitOfWork.CompleteAsync();
                return new AttendanceResponse(existingAttendance);
            }
            catch (Exception e)
            {
                return new AttendanceResponse("Has ocurred an error deleting the attendance " + e.Message);
            }
        }

        public async Task<AttendanceResponse> GetByEmployeeIdAndAttendanceIdAsync(int employeeId, int attendanceId)
        {
            var existingAttendance = await _attendanceRepository.FindByEmployeeIdAndAttendanceIdAsync(employeeId, attendanceId);
            if (existingAttendance == null)
                return new AttendanceResponse("Attendance not found");

            return new AttendanceResponse(existingAttendance);
        }

        public async Task<IEnumerable<Attendance>> ListAsync()
        {
            return await _attendanceRepository.ListAsync();
        }

        public async Task<IEnumerable<Attendance>> ListByEmployeeIdAndRangeAsync(int employeeId, DateTime startAt, DateTime endAt)
        {
            return await _attendanceRepository.ListByEmployeeIdAndRangeDateAsync(employeeId, startAt, endAt);
        }

        public async Task<IEnumerable<Attendance>> ListByEmployeeIdAsync(int employeeId)
        {
            return await _attendanceRepository.ListByEmployeeIdAsync(employeeId);
        }

        public async Task<AttendanceResponse> SaveAsync(int employeeId, Attendance attendance)
        {
            var existingEmployee = await _employeeRepository.FindById(employeeId);

            if (existingEmployee == null)
                return new AttendanceResponse("Employee Id not found");

            try
            {
                attendance.EmployeeId = employeeId;
                await _attendanceRepository.AddAsync(attendance);
                await _unitOfWork.CompleteAsync();
                return new AttendanceResponse(attendance);
            }
            catch (Exception e)
            {
                return new AttendanceResponse("Has ocurred an error saving the attendance " + e.Message);
            }
        }

        public async Task<AttendanceResponse> UpdateAsync(int employeeId, int id, Attendance attendance)
        {

            var existingAttendance = await _attendanceRepository.FindByEmployeeIdAndAttendanceIdAsync(employeeId, id);
            if (existingAttendance == null)
                return new AttendanceResponse("Attendance not found");

            try
            {
                existingAttendance.EntryTime = attendance.EntryTime;
                existingAttendance.OutTime = attendance.OutTime;
                existingAttendance.AttendanceDate = attendance.AttendanceDate;
                _attendanceRepository.Update(existingAttendance);
                await _unitOfWork.CompleteAsync();
                return new AttendanceResponse(existingAttendance);
            }
            catch (Exception e)
            {
                return new AttendanceResponse("Has ocurred an error updating the attendance " + e.Message);
            }
        }
    }
}
