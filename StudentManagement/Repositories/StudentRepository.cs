using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly hana_taekwondoContext _context;

    public StudentRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }

    public override Task<List<Student>> GetAll()
    {
        var results = _context.Students.ToListAsync();
        return results;
    }

    public Student GetStudentInfoByStudentId(int studentId)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
        return student;
    }

    public int AddNewStudent(Student student)
    {
        _context.Students.Add(student);
        var result = _context.SaveChanges();
        return result;
    }

    public void AddStudentTimetables(List<StudentTimetable> items)
    {
        _context.StudentTimetables.AddRange(items);
        _context.SaveChanges();
    }
}