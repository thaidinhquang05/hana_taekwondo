using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTOs.Output;
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
        var result = _context.Students.Include(x => x.Tuitions).ToList();
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

        foreach (var item in lastTuitions)
        {
            if (item != null)
            {
                if (item.DueDate >= currentDate && item.DueDate <= deadlineDate)
                {
                    var student = _context.Students.FirstOrDefault(s => s.Id == item.StudentId);
                    result.Add(student);
                }
            }
        }

        return result;
    }

    public List<AttendanceHistoryOutput> GetAttendanceHistory(int year)
    {
        var students = _context.Students.ToList();
        var result = students.Select((s, indx) => new AttendanceHistoryOutput()
        {
            Id = s.Id,
            Index = indx + 1,
            FullName = s.FullName,
        }).ToList();

        var attendance = _context.Attendances.Where(a => a.Date.Year == year).ToList();

        foreach (var item in result)
        {
            item.Jan = attendance.Count(a => a.Date.Month == 1 && a.StudentId == item.Id);
            item.Feb = attendance.Count(a => a.Date.Month == 2 && a.StudentId == item.Id);
            item.Mar = attendance.Count(a => a.Date.Month == 3 && a.StudentId == item.Id);
            item.Apr = attendance.Count(a => a.Date.Month == 4 && a.StudentId == item.Id);
            item.May = attendance.Count(a => a.Date.Month == 5 && a.StudentId == item.Id);
            item.Jun = attendance.Count(a => a.Date.Month == 6 && a.StudentId == item.Id);
            item.Jul = attendance.Count(a => a.Date.Month == 7 && a.StudentId == item.Id);
            item.Aug = attendance.Count(a => a.Date.Month == 8 && a.StudentId == item.Id);
            item.Sep = attendance.Count(a => a.Date.Month == 9 && a.StudentId == item.Id);
            item.Oct = attendance.Count(a => a.Date.Month == 10 && a.StudentId == item.Id);
            item.Nov = attendance.Count(a => a.Date.Month == 11 && a.StudentId == item.Id);
            item.Dec = attendance.Count(a => a.Date.Month == 12 && a.StudentId == item.Id);
        }

        return result;
    }
}