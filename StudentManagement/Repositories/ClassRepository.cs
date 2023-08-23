using Microsoft.EntityFrameworkCore;
using StudentManagement.DTOs.Input;
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

    public List<Class> GetAllClasses()
    {
        return _context.Classes.ToList();
    }

    public void AddStudentToClass(List<int> _studentIds, int _classId)
    {
        var _class = _context.Classes.Where(s => s.Id == _classId).FirstOrDefault() ?? throw new Exception("Not found class!");

        foreach (var item in _studentIds)
        {
            var _student = _context.Students.Where(s => s.Id == item).FirstOrDefault() ?? throw new Exception("Not found student!");
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
    }

    public void RemoveStudentFromClass(Student _student)
    {
        var studentClass = _context.StudentClasses.Where(sc => sc.StudentId == _student.Id).ToList() ?? throw new NullReferenceException("Record not found!");
        _context.StudentClasses.RemoveRange(studentClass);
        _context.SaveChanges();
    }

    public List<Class> FindClassByKeyWord(string keyword)
    {
        return _context.Classes.Where(c => c.Name.Contains(keyword)).ToList() ?? throw new NullReferenceException("Class not found!");
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
            StartDate = x.Class.StartDate.ToShortDateString(),
            DueDate = x.Class.DueDate.ToShortDateString()
        })
        .ToList() ?? throw new NullReferenceException("Not found classes!");
        return result;
    }

    public Class GetClassById(int classId)
    {
        var result = _context.Classes.Where(c => c.Id == classId).FirstOrDefault() ?? throw new NullReferenceException("Not found class!");
        return result;
    }

    public void DeleteClass(int classId)
    {
        var _class = _context.Classes.FirstOrDefault(c => c.Id == classId) ?? throw new NullReferenceException("Not found class!");
        _context.Classes.Remove(_class);
        _context.SaveChanges();
    }

    public void AddNewClass(NewClassInput newClassInput)
    {
        Class _class = new Class
        { 
            Name = newClassInput.Name, 
            Desc = newClassInput.Desc, 
            CreatedAt = DateTime.Now, 
            DueDate = newClassInput.DueDate, 
            ModifiedAt = DateTime.Now,
            StartDate = newClassInput.StartDate
        };
        _context.Classes.Add(_class);
        _context.SaveChanges();
    }
}