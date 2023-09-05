using Microsoft.EntityFrameworkCore;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class TimetableRepository : Repository<Timetable>, ITimetableRepository
{
    private readonly hana_taekwondoContext _context;

    public TimetableRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }

    public List<TimetableOutput> GetAllTimetables()
    {
        var slots = _context.Slots
            .Include(x => x.Timetables)
            .Select(slot => new TimetableOutput { Slot = slot })
            .ToList();
        return slots;
    }

    public List<StudentTimetableOutput> GetTimetableByStudentId(int studentId)
    {
        var slots = _context.StudentTimetables
            .Include(x => x.TimeTable)
            .ThenInclude(x => x.Slot)
            .Where(x => x.StudentId == studentId)
            .Select(slot => new StudentTimetableOutput
            {
                TimetableId = slot.TimeTableId
            })
            .ToList();
        return slots;
    }

    public void AddStudentTimetables(List<StudentTimetable> input)
    {
        _context.StudentTimetables.AddRange(input);
        _context.SaveChanges();
    }

    public void RemoveStudentTimetables(int studentId)
    {
        var studentTimetables = _context.StudentTimetables
            .Where(x => x.StudentId == studentId).ToList();
        _context.StudentTimetables.RemoveRange(studentTimetables);
        _context.SaveChanges();
    }
}