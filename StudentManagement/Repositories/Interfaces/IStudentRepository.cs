using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Student GetStudentInfoByStudentId(int studentId);
    
    int AddNewStudent(Student student);

    void AddStudentTimetables(List<StudentTimetable> items);
}