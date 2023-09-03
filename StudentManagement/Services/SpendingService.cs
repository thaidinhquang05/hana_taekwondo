using StudentManagement.DTOs.Output;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class SpendingService : ISpendingService
{
    private readonly ISpendingRepository _repository;

    public SpendingService(ISpendingRepository repository)
    {
        _repository = repository;
    }

    public SpendingValueOutput GetSpendingValue(int month, int year)
    {
        var result = _repository.GetSpendingValue(month, year);
        return result;
    }
}