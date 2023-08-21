using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    List<Student> GetAllStudents();
    
    Student GetStudentInfoByStudentId(int studentId);

    int AddNewStudent(Student student);

    int UpdateStudent(Student student);
}