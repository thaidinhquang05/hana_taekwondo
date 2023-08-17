using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    int AddNewStudent(Student student);

    void AddStudentTimetables(List<StudentTimetable> items);
}