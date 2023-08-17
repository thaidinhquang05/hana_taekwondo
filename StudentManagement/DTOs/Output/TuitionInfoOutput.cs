namespace StudentManagement.DTOs.Output;

public class TuitionInfoOutput
{
    public DateTime PaidDate { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public decimal Amount { get; set; }
    
    public decimal ActualAmount { get; set; }
    
    public string? Content { get; set; }
    
    public string? Note { get; set; }
}