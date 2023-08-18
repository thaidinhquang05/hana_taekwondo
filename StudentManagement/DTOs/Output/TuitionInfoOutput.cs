namespace StudentManagement.DTOs.Output;

public class TuitionInfoOutput
{
    public string PaidDate { get; set; }
    
    public string DueDate { get; set; }
    
    public decimal Amount { get; set; }
    
    public decimal ActualAmount { get; set; }
    
    public string? Content { get; set; }
    
    public string? Note { get; set; }
}