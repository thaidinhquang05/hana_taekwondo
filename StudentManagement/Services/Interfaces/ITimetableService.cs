using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;

namespace StudentManagement.Services.Interfaces;

public interface ITimetableService
{
    List<TimetableOutput> GetAllTimetables();
    
    List<StudentTimetableOutput> GetTimetableByStudentId(int studentId);

    void UpdateStudentTimetables(int studentId, List<TimetableInput> input);
}