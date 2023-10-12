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

    public List<StudentAttendanceOutput> GetStudentBySlotAndDate(int slotId, DateTime date, string daysOfWeek)
    {
        List<StudentAttendanceOutput> result = new List<StudentAttendanceOutput>();
        int index = 1;

        var timetables = _context.Timetables.Where(s => s.SlotId == slotId && s.WeekDay.Equals(daysOfWeek)).ToList();
        List<Student> students = new List<Student>();

        foreach (var timetable in timetables)
        {
            var student = _context.StudentTimetables.Where(st => st.TimeTableId == timetable.Id).Select(st => st.Student).ToList();
            students.AddRange(student);
        }

        foreach (var item in students)
        {
            StudentAttendanceOutput studentAttendanceOutput = new StudentAttendanceOutput();
            studentAttendanceOutput.Index = index;
            studentAttendanceOutput.Id = item.Id;
            studentAttendanceOutput.FullName = item.FullName;
            studentAttendanceOutput.StudentImg = item.StudentImg;
            studentAttendanceOutput.Gender = item.Gender ? "Male" : "Female";

            var attend = _context.Attendances.Where(a => a.Date == date && a.SlotId == slotId && a.StudentId == item.Id).FirstOrDefault();

            if (attend != null)
            {
                studentAttendanceOutput.Note = attend.Note;
                studentAttendanceOutput.IsAttend = attend.IsAttendance;
            }
            else
            {
                studentAttendanceOutput.Note = String.Empty;
                studentAttendanceOutput.IsAttend = null;
            }

            result.Add(studentAttendanceOutput);
            index++;
        }

        return result;
    }

    public List<StudentAttendanceOutput> GetStudentMakeUpBySlotAndDate(DateTime date)
    {
        var result = new List<StudentAttendanceOutput>();
        var lastTuitions = _context.Students.Select(student => student.Tuitions
             .OrderByDescending(tuition => tuition.DueDate)
             .FirstOrDefault()).ToList();
        int index = 1;

        foreach (var item in lastTuitions)
        {
            if (item != null)
            {
                if (item.PaidDate <= date && date <= item.DueDate)
                {
                    var student = _context.Students
                        .Where(s => s.Attendances.Any(a => a.IsAttendance == false && a.Date < date)).
                        FirstOrDefault(s => s.Id == item.StudentId);
                    if (student != null)
                    {
                        StudentAttendanceOutput studentAttendanceOutput = new StudentAttendanceOutput();
                        studentAttendanceOutput.Index = index;
                        studentAttendanceOutput.Id = student.Id;
                        studentAttendanceOutput.FullName = student.FullName;
                        studentAttendanceOutput.StudentImg = student.StudentImg;
                        studentAttendanceOutput.Gender = student.Gender ? "Male" : "Female";
                        studentAttendanceOutput.Note = String.Empty;

                        result.Add(studentAttendanceOutput);
                    }
                }
            }
            index++;
        }

        return result;
    }

    public void TakeAttendance(int slotId, DateTime date, List<StudentAttendanceInput> studentAttendanceInputs)
    {

        foreach (var item in studentAttendanceInputs)
        {
            var attendance = _context.Attendances.Where(a => a.SlotId == slotId && a.Date == date && a.StudentId == item.Id).FirstOrDefault();

            if (attendance == null)
            {
                Attendance newAttendance = new Attendance();
                newAttendance.SlotId = slotId;
                newAttendance.Date = date;
                newAttendance.StudentId = item.Id;
                newAttendance.Student = _context.Students.Where(s => s.Id == item.Id).FirstOrDefault();
                newAttendance.IsAttendance = item.IsAttend;
                newAttendance.Slot = _context.Slots.Where(s => s.Id == slotId).FirstOrDefault();
                newAttendance.Note = item.Note;
                _context.Attendances.Add(newAttendance);
                _context.SaveChanges();
            }
            else
            {
                attendance.Note = item.Note;
                attendance.IsAttendance = item.IsAttend;
                _context.Attendances.Update(attendance);
                _context.SaveChanges();
            }
        }
    }

    public void TakeMakeUpAttendance(int slotId, DateTime date, List<StudentAttendanceInput> studentAttendanceInputs)
    {

        foreach (var item in studentAttendanceInputs)
        {
            var tution = _context.Tuitions.Where(t => t.StudentId == item.Id).OrderByDescending(t => t.DueDate).FirstOrDefault();
            var attendance = _context.Attendances.Include(a => a.Student).Where(a => a.IsAttendance == false
            && a.StudentId == item.Id
            && tution.PaidDate < a.Date
            && a.Date < date).FirstOrDefault();
            if (attendance != null)
            {
                attendance.Note = item.Note;
                attendance.IsAttendance = item.IsAttend;
                attendance.Slot = _context.Slots.Where(s => s.Id == slotId).FirstOrDefault();
                attendance.SlotId = slotId;
                attendance.Date = date;
                _context.Attendances.Update(attendance);
                _context.SaveChanges();
            }
        }
    }
}