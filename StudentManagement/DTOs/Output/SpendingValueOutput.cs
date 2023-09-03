namespace StudentManagement.DTOs.Output;

public class SpendingValueOutput
{
    public SpendingItemValueOutput Monthly { get; set; }

    public List<decimal> SpendingData { get; set; }

    public decimal SpendingAnnual { get; set; }
}