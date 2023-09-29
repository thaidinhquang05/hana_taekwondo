using StudentManagement.DTOs.Output;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class SlotService : ISlotService
{
    private readonly ISlotRepository _repository;

    public SlotService(ISlotRepository repository)
    {
        _repository = repository;
    }

    public List<SlotOutput> GetSlots()
    {
        var result = _repository.GetSlots()
            .Select((x,index) => new SlotOutput
            {
                Index = index + 1,
                Id = x.Id,
                SlotDescription = x.Desc
            }).ToList();
        return result;
    }
}