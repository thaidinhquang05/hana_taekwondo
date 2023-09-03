namespace StudentManagement.DTOs.Output;

public class EarningValue
{
    public decimal Monthly { get; set; }
    
    public List<decimal> EarningData { get; set; }

    public decimal Annual { get; set; }
}