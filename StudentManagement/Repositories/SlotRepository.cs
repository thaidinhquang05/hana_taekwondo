using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class SlotRepository : Repository<Slot>, ISlotRepository
{
    private readonly hana_taekwondoContext _context;
    
    public SlotRepository(hana_taekwondoContext context) : base(context)
    {
        _context = context;
    }

    public List<Slot> GetSlots()
    {
        var result = _context.Slots.ToList();
        return result;
    }
}