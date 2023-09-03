using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class SpendingRepository : Repository<Spending>, ISpendingRepository
{
    private readonly hana_taekwondoContext _context;
    
    public SpendingRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }

    public SpendingValueOutput GetSpendingValue(int month, int year)
    {
        var monthlyTotals = _context.Spendings
            .Where(x => x.PaidDate.Year == year && x.PaidDate.Month == month)
            .GroupBy(x => x.PaidDate.Month)
            .Select(group => new SpendingItemValueOutput
            {
                Month = group.Key,
                ElectricSpending = group.Sum(x => x.Electric),
                WaterSpending = group.Sum(x => x.Water),
                RentSpending = group.Sum(x => x.Rent),
                AnotherSpending = group.Sum(x => x.Another),
                PaidDate = $"{year}-{group.Key:00}",
                Total = group.Sum(x => x.Water + x.Electric + x.Rent + x.Another)
            })
            .FirstOrDefault();
        
        var annual = _context.Spendings
            .Where(x => x.PaidDate.Year == year)
            .Sum(x => x.Water + x.Electric + x.Rent + x.Another);
        
        var allMonths = Enumerable.Range(1, 12);
        var spendingData = allMonths
            .GroupJoin(
                _context.Spendings.Where(x => x.PaidDate.Year == year),
                month => month,
                tuition => tuition.PaidDate.Month,
                (month, tuitions) => new
                {
                    Month = month,
                    TotalSpendings = tuitions.Sum(x => x.Water + x.Electric + x.Rent + x.Another)
                }
            )
            .OrderBy(x => x.Month)
            .ToList();

        var spendingValues = spendingData.Select(item => item.TotalSpendings).ToList();

        monthlyTotals ??= new SpendingItemValueOutput
        {
            Month = month,
            ElectricSpending = 0,
            WaterSpending = 0,
            RentSpending = 0,
            AnotherSpending = 0,
            PaidDate = $"{year}-{month}",
            Total = 0
        };
        return new SpendingValueOutput
        {
            Monthly = monthlyTotals,
            SpendingData = spendingValues,
            SpendingAnnual = annual
        };
    }
}