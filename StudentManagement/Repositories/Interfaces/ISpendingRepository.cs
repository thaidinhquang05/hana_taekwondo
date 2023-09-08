using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface ISpendingRepository : IRepository<Spending>
{
    SpendingValueOutput GetSpendingValue(int month, int year);

    IEnumerable<Spending> GetListSpending();

    Spending GetSpendingById(int spendingId);
}