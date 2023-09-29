using DocumentFormat.OpenXml.Wordprocessing;
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
        var _class = _context.Classes.Where(s => s.Id == _classId).FirstOrDefault();

        foreach (var item in _studentIds)
        {
            var _student = _context.Students.Where(s => s.Id == item).FirstOrDefault();
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

    public void RemoveStudentFromClass(int _studentId, int _classId)
    {
        var studentClass = _context.StudentClasses.Where(sc => sc.StudentId == _studentId && sc.ClassId == _classId)
            .FirstOrDefault();
        _context.StudentClasses.Remove(studentClass);
        _context.SaveChanges();
    }

    public List<Class> FindClassByKeyWord(string keyword)
    {
        return _context.Classes.Where(c => c.Name.Contains(keyword)).ToList();
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
            .ToList();
        return result;
    }

    public Class GetClassById(int classId)
    {
        var result = _context.Classes.Where(c => c.Id == classId).FirstOrDefault();
        return result;
    }

    public void DeleteClass(int classId)
    {
        var _classStudent = _context.StudentClasses.Where(sc => sc.ClassId == classId).ToList();
        if (_classStudent != null)
        {
            _context.StudentClasses.RemoveRange(_classStudent);
            _context.SaveChanges();
        }

        var _class = _context.Classes.FirstOrDefault(c => c.Id == classId);
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

    public List<Class> GetClassesByDate(DateTime date)
    {
        var result = _context.Classes
            .Where(x => x.StartDate <= date && x.DueDate >= date)
            .ToList();
        return result;
    }

    public List<StudentAttendanceOutput> GetStudentByClassAndDate(int classId, DateTime date)
    {
        List<StudentAttendanceOutput> result = new List<StudentAttendanceOutput>();
        int index = 1;

        var students = _context.StudentClasses
            .Where(s => s.ClassId == classId)
            .Select(sc => sc.Student)
            .ToList();

        // foreach (var item in students)
        // {
        //     StudentAttendanceOutput studentAttendanceOutput = new StudentAttendanceOutput();
        //     studentAttendanceOutput.Index = index;
        //     studentAttendanceOutput.Id = item.Id;
        //     studentAttendanceOutput.FullName = item.FullName;
        //     studentAttendanceOutput.StudentImg = item.StudentImg;
        //     studentAttendanceOutput.Gender = item.Gender ? "Male" : "Female";
        //
        //     var attend = _context.Attendances
        //         .Where(a => a.Date == date && a.ClassId == classId && a.StudentId == item.Id)
        //         .FirstOrDefault();
        //
        //     if (attend != null)
        //     {
        //         studentAttendanceOutput.Note = attend.Note;
        //         studentAttendanceOutput.IsAttend = attend.IsAttendance;
        //     }
        //     else
        //     {
        //         studentAttendanceOutput.Note = String.Empty;
        //         studentAttendanceOutput.IsAttend = null;
        //     }
        //
        //     result.Add(studentAttendanceOutput);
        //     index++;
        // }

        return result;
    }

    public void TakeAttendance(int classId, DateTime date, List<StudentAttendanceInput> studentAttendanceInputs)
    {

        // foreach (var item in studentAttendanceInputs)
        // {
        //     var attendance = _context.Attendances.Where(a => a.ClassId == classId && a.Date == date && a.StudentId == item.Id).FirstOrDefault();
        //
        //     if (attendance == null)
        //     {
        //         Attendance newAttendance = new Attendance();
        //         newAttendance.ClassId = classId;
        //         newAttendance.Date = date;
        //         newAttendance.StudentId = item.Id;
        //         newAttendance.Student = _context.Students.Where(s => s.Id == item.Id).FirstOrDefault();
        //         newAttendance.IsAttendance = item.IsAttend;
        //         newAttendance.Class = _context.Classes.Where(s => s.Id == classId).FirstOrDefault();
        //         newAttendance.ClassId = classId;
        //         newAttendance.Note = item.Note;
        //         _context.Attendances.Add(newAttendance);
        //         _context.SaveChanges();
        //     }
        //     else
        //     {
        //         attendance.Note = item.Note;
        //         attendance.IsAttendance = item.IsAttend;
        //         _context.Attendances.Update(attendance);
        //         _context.SaveChanges();
        //     }
        // }
    }
}