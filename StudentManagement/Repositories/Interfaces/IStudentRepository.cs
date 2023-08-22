using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    List<Student> GetAllStudents();
    
    Student GetStudentInfoByStudentId(int studentId);

    int AddNewStudent(Student student);

    int UpdateStudent(Student student);

    void AddStudentTimetables(IEnumerable<StudentTimetable> items);

    List<StudentTimetable> GetStudentTimetablesByStudentId(int studentId);

    void DeleteStudentTimetables(List<StudentTimetable> items);
    public List<Student> GetStudentByClass(int classId);
    public List<Student> GetStudentToAddClass(int classId);
}