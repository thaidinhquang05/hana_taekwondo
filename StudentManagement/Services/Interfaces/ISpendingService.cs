using StudentManagement.DTOs.Output;

namespace StudentManagement.Services.Interfaces;

public interface ISpendingService
{
    SpendingValueOutput GetSpendingValue(int month, int year);
}