namespace StudentManagement.DTOs.Output;

public class ClassInfoOutput
{
    public string Name { get; set; }
    
    public string? Desc { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime DueDate { get; set; }
}