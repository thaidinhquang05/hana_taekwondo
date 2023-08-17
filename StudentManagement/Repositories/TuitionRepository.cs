using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class TuitionRepository : ITuitionRepository
{
    private readonly hana_taekwondoContext _context;

    public TuitionRepository(hana_taekwondoContext context)
    {
        _context = context;
    }
    
    public int AddNewTuition(Tuition tuition)
    {
        _context.Tuitions.Add(tuition);
        var result = _context.SaveChanges();
        return result;
    }
}