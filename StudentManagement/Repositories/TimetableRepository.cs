using Microsoft.EntityFrameworkCore;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class TimetableRepository : ITimetableRepository
{
    private readonly hana_taekwondoContext _context;

    public TimetableRepository(hana_taekwondoContext context)
    {
        _context = context;
    }

    public List<TimetableOutput> GetTimetableByStudentId(int studentId)
    {
        var result = _context.StudentTimetables
            .Include(x => x.TimeTable)
            .ThenInclude(x => x.Slot)
            .Where(x => x.StudentId == studentId)
            .Select(x => new TimetableOutput
            {
                WeekDay = x.TimeTable.WeekDay,
                SlotDesc = x.TimeTable.Slot.Desc
            })
            .ToList();

        return result;
    }

    public void ChooseTimeTable(Student student, List<TimetableSelectionInput> timetables)
    {
        foreach(var item in timetables)
        {
            Timetable timetable = new Timetable();
            StudentTimetable studentTimetable = new StudentTimetable();
            foreach(var slot in item.SlotIds)
            {
                timetable.WeekDay = item.Day;
                timetable.SlotId = slot;

                studentTimetable.Student = student;
                studentTimetable.StudentId  = student.Id;

                timetable.StudentTimetables.Add(studentTimetable);
                _context.Timetables.Add(timetable);
                _context.SaveChanges();
            }
        }
    }
}