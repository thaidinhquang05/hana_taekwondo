using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface IAttendanceRepository : IRepository<Attendance>
{
    List<Attendance> GetAttendanceByStudentId(int studentId);
}