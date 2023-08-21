using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class TuitionRepository : Repository<Tuition>, ITuitionRepository
{
    private readonly hana_taekwondoContext _context;

    public TuitionRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }

    public int AddNewTuition(Tuition tuition)
    {
        _context.Tuitions.Add(tuition);
        var result = _context.SaveChanges();
        return result;
    }

    public List<TuitionInfoOutput> GetTuitionByStudentId(int studentId)
    {
        var result = _context.Tuitions
            .Where(x => x.StudentId == studentId)
            .Select(x => new TuitionInfoOutput
            {
                Id = x.Id,
                PaidDate = $"{x.PaidDate:yyyy-MM-dd}",
                DueDate = $"{x.DueDate:yyyy-MM-dd}",
                Amount = x.Amount,
                ActualAmount = x.ActualAmount,
                Content = x.Content,
                Note = x.Note
            })
            .ToList();
        return result;
    }
}