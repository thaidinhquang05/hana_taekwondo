using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface ITimetableRepository : IRepository<Timetable>
{
    List<TimetableOutput> GetTimetableByStudentId(int studentId);
}