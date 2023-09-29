using StudentManagement.DTOs.Output;

namespace StudentManagement.Services.Interfaces;

public interface ISlotService
{
    List<SlotOutput> GetSlots();
}