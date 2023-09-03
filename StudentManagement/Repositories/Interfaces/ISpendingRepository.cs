using StudentManagement.DTOs.Output;

namespace StudentManagement.Repositories.Interfaces;

public interface ISpendingRepository
{
    SpendingValueOutput GetSpendingValue(int month, int year);
}