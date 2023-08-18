using Microsoft.EntityFrameworkCore;
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
}