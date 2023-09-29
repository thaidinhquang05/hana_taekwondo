using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface ISlotRepository : IRepository<Slot>
{
    List<Slot> GetSlots();
}