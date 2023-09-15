namespace StudentManagement.DTOs.Input;

public class SpendingInput
{
    public decimal Electric { get; set; }

    public decimal Water { get; set; }

    public decimal Rent { get; set; }
    
    public decimal Salary { get; set; }
    
    public decimal Eating { get; set; }
    
    public decimal Another { get; set; }

    public DateTime PaidDate { get; set; }

    public string? Content { get; set; }
}