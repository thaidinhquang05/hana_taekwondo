using StudentManagement.DTOs.Output;

namespace StudentManagement.Repositories.Interfaces;

public interface ITimetableRepository
{
    List<TimetableOutput> GetTimetableByStudentId(int studentId);
}