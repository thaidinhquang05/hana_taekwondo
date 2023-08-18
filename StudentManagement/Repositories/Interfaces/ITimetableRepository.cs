using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface ITimetableRepository
{
    List<TimetableOutput> GetTimetableByStudentId(int studentId);
    public void ChooseTimeTable(Student student, List<TimetableSelectionInput> timetables);
}