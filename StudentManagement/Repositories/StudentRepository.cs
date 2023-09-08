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

    public List<Student> GetAllStudents()
    {
        var result = _context.Students.ToList();
        return result;
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

    public int UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        var result = _context.SaveChanges();
        return result;
    }

    public void DeleteStudentClass(List<StudentClass> entities)
    {
        _context.StudentClasses.RemoveRange(entities);
        _context.SaveChanges();
    }

    public override async Task Delete(Student entity)
    {
        _context.Students.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public List<StudentClass> GetStudentClassesByStudentId(int studentId)
    {
        var result = _context.StudentClasses.Where(x => x.StudentId == studentId).ToList();
        return result;
    }

    public List<StudentTimetable> GetStudentTimetablesByStudentId(int studentId)
    {
        var result = _context.StudentTimetables
            .Where(x => x.StudentId == studentId).ToList();
        return result;
    }

    public void AddStudentTimetables(IEnumerable<StudentTimetable> items)
    {
        _context.StudentTimetables.AddRange(items);
        _context.SaveChanges();
    }

    public void DeleteStudentTimetables(List<StudentTimetable> items)
    {
        _context.StudentTimetables.RemoveRange(items);
        _context.SaveChanges();
    }

    public List<Student> GetStudentByClass(int classId)
    {
        return _context.StudentClasses.Where(sc => sc.ClassId == classId).Select(sc => sc.Student).ToList();
    }

    public List<Student> GetStudentToAddClass(int classId)
    {
        var result = _context.Students.Where(student =>
            !_context.StudentClasses.Any(sc => sc.StudentId == student.Id && sc.ClassId == classId)).ToList();

        return result;
    }

    public List<Student> GetUpcomingDeadlinesStudent()
    {
        DateTime currentDate = DateTime.Now;

        DateTime deadlineDate = currentDate.AddDays(5);

        List<Student> result = new();

        var lastTuitions = _context.Students.Select(student => student.Tuitions
                .OrderByDescending(tuition => tuition.DueDate)
                .FirstOrDefault()).ToList();

        foreach(var item in lastTuitions)
        {
            if(item.DueDate >= currentDate && item.DueDate <= deadlineDate)
            {
                var student = _context.Students.FirstOrDefault(s => s.Id == item.StudentId);
                result.Add(student);
            }
        }

        return result;
    }
}