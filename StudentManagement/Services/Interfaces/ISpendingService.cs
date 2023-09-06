using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces;

public interface ISpendingService
{
    SpendingValueOutput GetSpendingValue(int month, int year);

    List<SpendingItemListOutput> GetListSpending();

    SpendingItemListOutput GetSpendingItem(int spendingId);

    void AddNewSpending(SpendingInput input);

    void UpdateSpending(int spendingId, SpendingInput input);

    void DeleteSpendingRecord(int spendingId);
}