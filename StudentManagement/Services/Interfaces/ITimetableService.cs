using StudentManagement.DTOs.Output;

namespace StudentManagement.Services.Interfaces;

public interface ITimetableService
{
    List<TimetableOutput> GetTimetableByStudentId(int studentId);
}