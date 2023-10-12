using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    List<Student> GetAllStudents();

    Student GetStudentInfoByStudentId(int studentId);

    int AddNewStudent(Student student);

    int UpdateStudent(Student student);

    void DeleteStudentClass(List<StudentClass> entities);

    void AddStudentTimetables(IEnumerable<StudentTimetable> items);

    List<StudentClass> GetStudentClassesByStudentId(int studentId);

    List<StudentTimetable> GetStudentTimetablesByStudentId(int studentId);

    void DeleteStudentTimetables(List<StudentTimetable> items);

    List<Student> GetStudentByClass(int classId);

    List<Student> GetStudentToAddClass(int classId);

    List<Student> GetUpcomingDeadlinesStudent();
    
    List<AttendanceHistoryOutput> GetAttendanceHistory(int year);
}