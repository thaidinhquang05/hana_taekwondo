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

    public List<Tuition> GetTuitionByStudentId(int studentId)
    {
        var result = _context.Tuitions
            .Where(x => x.StudentId == studentId)
            .ToList();
        return result;
    }

    public void DeleteTuition(List<Tuition> entities)
    {
        _context.Tuitions.RemoveRange(entities);
        _context.SaveChanges();
    }

    public Tuition GetTuitionById(int tuitionId)
    {
        var tuition = _context.Tuitions.FirstOrDefault(x => x.Id == tuitionId);
        return tuition;
    }

    public EarningValue GetEarningValueByMonth(int month, int year)
    {
        var monthly = _context.Tuitions
            .Where(x => x.PaidDate.Month == month && x.PaidDate.Year == year)
            .Sum(x => x.ActualAmount);

        var annual = _context.Tuitions
            .Where(x => x.PaidDate.Year == year)
            .Sum(x => x.ActualAmount);

        return new EarningValue
        {
            Monthly = monthly,
            Annual = annual
        };
    }

    public override async Task Update(Tuition entity)
    {
        _context.Tuitions.Update(entity);
        await _context.SaveChangesAsync();
    }
}