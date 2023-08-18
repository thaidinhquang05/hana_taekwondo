using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Student GetStudentInfoByStudentId(int studentId);

    int AddNewStudent(Student student);

    int UpdateStudent(Student student);

    void AddStudentTimetables(IEnumerable<StudentTimetable> items);

    List<StudentTimetable> GetStudentTimetablesByStudentId(int studentId);

    void DeleteStudentTimetables(List<StudentTimetable> items);
}