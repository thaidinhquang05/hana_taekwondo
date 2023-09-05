using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface ITimetableRepository : IRepository<Timetable>
{
    List<TimetableOutput> GetAllTimetables();
    
    List<StudentTimetableOutput> GetTimetableByStudentId(int studentId);

    void AddStudentTimetables(List<StudentTimetable> input);
    
    void RemoveStudentTimetables(int studentId);
}