using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly hana_taekwondoContext _context;

    public StudentRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }
    
    public override Task<List<Student>> GetAll()
    {
        var results = _context.Students.ToListAsync();
        return results;
    }
    
    public override Task Add(Student entity)
    {
        _context.Students.Add(entity);
        _context.SaveChangesAsync();
        throw new Exception("Have something wrong when add new student!");
    }
}