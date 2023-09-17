using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class SpendingService : ISpendingService
{
    private readonly ISpendingRepository _repository;
    private readonly IMapper _mapper;

    public SpendingService(ISpendingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public SpendingValueOutput GetSpendingValue(int month, int year)
    {
        var result = _repository.GetSpendingValue(month, year);
        return result;
    }

    public List<SpendingItemListOutput> GetListSpending()
    {
        var spendingList = _repository.GetListSpending();
        var result = spendingList.Select((x, i) => new SpendingItemListOutput
            {
                Id = x.Id,
                Index = i + 1,
                Electric = x.Electric,
                Water = x.Water,
                Rent = x.Rent,
                Salary = x.Salary,
                Eating = x.Eating,
                Another = x.Another,
                PaidDate = $"{x.PaidDate: dd-MM-yyyy}",
                Content = x.Content
            })
            .ToList();
        return result;
    }

    public SpendingItemListOutput GetSpendingItem(int spendingId)
    {
        var spending = _repository.GetSpendingById(spendingId);
        if (spending is null)
        {
            throw new Exception($"Spending record with id {spendingId} is not exist!");
        }

        return _mapper.Map<SpendingItemListOutput>(spending);
    }

    public void AddNewSpending(SpendingInput input)
    {
        var newSpending = _mapper.Map<Spending>(input);
        _repository.Add(newSpending);
    }

    public void UpdateSpending(int spendingId, SpendingInput input)
    {
        var existedSpending = _repository.GetSpendingById(spendingId);
        if (existedSpending is null)
        {
            throw new Exception($"Spending with id: {spendingId} is not exist!");
        }

        existedSpending.Electric = input.Electric;
        existedSpending.Water = input.Water;
        existedSpending.Rent = input.Rent;
        existedSpending.Salary = input.Salary;
        existedSpending.Eating = input.Eating;
        existedSpending.Another = input.Another;
        existedSpending.PaidDate = input.PaidDate;
        existedSpending.Content = input.Content;
        existedSpending.ModifiedAt = DateTime.Now;
        _repository.Update(existedSpending);
    }

    public void DeleteSpendingRecord(int spendingId)
    {
        var spending = _repository.GetSpendingById(spendingId);
        if (spending is null)
        {
            throw new Exception($"Spending with id: {spendingId} is not exist!");
        }

        _repository.Delete(spending);
    }
}