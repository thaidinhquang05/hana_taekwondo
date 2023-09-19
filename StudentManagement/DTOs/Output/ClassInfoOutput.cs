namespace StudentManagement.DTOs.Output;

public class ClassInfoOutput
{
    public int Index { get; set; }
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string? Desc { get; set; }
    
    public string StartDate { get; set; }
    
    public string DueDate { get; set; }
}