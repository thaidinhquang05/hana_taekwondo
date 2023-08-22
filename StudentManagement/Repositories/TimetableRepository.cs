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

    public List<TimetableOutput> GetTimetableByStudentId(int studentId)
    {
        var slots = _context.StudentTimetables
            .Include(x => x.TimeTable)
            .ThenInclude(x => x.Slot)
            .Where(x => x.StudentId == studentId)
            .Select(slot => new TimetableOutput
            {
                Slot = slot.TimeTable.Slot
            })
            .ToList();
        return slots;
    }

    public void ChooseTimeTable(Student student, List<TimetableSelectionInput> timetables)
    {
        foreach (var item in timetables)
        {
            Timetable timetable = new Timetable();
            StudentTimetable studentTimetable = new StudentTimetable();
            foreach (var slot in item.SlotIds)
            {
                timetable.WeekDay = item.Day;
                timetable.SlotId = slot;

                studentTimetable.Student = student;
                studentTimetable.StudentId = student.Id;

                timetable.StudentTimetables.Add(studentTimetable);
                _context.Timetables.Add(timetable);
                _context.SaveChanges();
            }
        }
    }
}