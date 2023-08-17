using Microsoft.EntityFrameworkCore;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class ClassRepository : IClassRepository
{
    private readonly hana_taekwondoContext _context;

    public ClassRepository(hana_taekwondoContext context)
    {
        _context = context;
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