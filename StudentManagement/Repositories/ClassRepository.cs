using Microsoft.EntityFrameworkCore;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class ClassRepository : Repository<Class>, IClassRepository
{

    private readonly hana_taekwondoContext _context;

    public ClassRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }
    
    public void addStudentToClass(Student _student, Class _class)
    {
        StudentClass studentClass = new StudentClass();
        studentClass.Student = _student;
        studentClass.Class = _class;
        studentClass.ClassId = _class.Id;
        studentClass.StudentId = _student.Id;
        studentClass.CreatedAt = DateTime.Now;
        studentClass.ModifiedAt = DateTime.Now;
        _context.StudentClasses.Add(studentClass);
        _context.SaveChanges();
    }

    public void removeStudentFromClass(Student _student) {
        var studentClass = _context.StudentClasses.Where(sc => sc.StudentId == _student.Id).ToList() ?? throw new NullReferenceException("Record not found!");
        _context.StudentClasses.RemoveRange(studentClass);
        _context.SaveChanges();
    }


    public List<ClassInfoOutput> GetClassesByStudentId(int studentId)
    {
        var result = _context.StudentClasses
        .Include(x => x.Class)
        .Where(x => x.StudentId == studentId)
        .Select(x => new ClassInfoOutput
        {
            Name = x.Class.Name,
            Desc = x.Class.Desc,
            StartDate = x.Class.StartDate,
            DueDate = x.Class.DueDate
        })
        .ToList();
        return result;
    }
}