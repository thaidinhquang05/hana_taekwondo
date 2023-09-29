using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
{
    private readonly hana_taekwondoContext _context;

    public AttendanceRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }


    public List<Attendance> GetAttendanceByStudentId(int studentId)
    {
        var result = _context.Attendances
            .Where(a => a.StudentId == studentId)
            .Include(x => x.Slot)
            .Include(x => x.Student)
            .ToList();
        return result;
    }
}