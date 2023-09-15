namespace StudentManagement.DTOs.Output;

public class SpendingItemListOutput
{
    public int Id { get; set; }
    
    public decimal Electric { get; set; }
    
    public decimal Water { get; set; }
    
    public decimal Rent { get; set; }
    
    public decimal Salary { get; set; }
    
    public decimal Eating { get; set; }
    
    public decimal Another { get; set; }
    
    public string PaidDate { get; set; }
    
    public string? Content { get; set; }
}