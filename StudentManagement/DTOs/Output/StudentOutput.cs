namespace StudentManagement.DTOs.Output;

public class StudentOutput
{
    public int Id { get; set; }
    
    public int Index { get; set; }

    public string StudentImg { get; set; }
    
    public string FullName { get; set; }
    
    public string Dob { get; set; }
    
    public string Gender { get; set; }
    
    public string? ParentName { get; set; }
    
    public string? Phone { get; set; }
    
    public decimal TotalTuitions { get; set; }
}