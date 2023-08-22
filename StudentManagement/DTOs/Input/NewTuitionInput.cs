using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DTOs.Input;

public class NewTuitionInput
{
    [Required(ErrorMessage = "You need to choose paid date!")]
    public DateTime PaidDate { get; set; }
    
    [Required(ErrorMessage = "You need to choose due date!")]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "You need to fill amount!")]
    public decimal Amount { get; set; }
    
    [Required(ErrorMessage = "You need to fill actual amount!")]
    public decimal ActualAmount { get; set; }
    
    public string? Content { get; set; }
    
    public string? Note { get; set; }
}