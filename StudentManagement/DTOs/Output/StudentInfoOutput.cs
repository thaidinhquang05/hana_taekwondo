namespace StudentManagement.DTOs.Output;

public class StudentInfoOutput
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Dob { get; set; }

    public string Gender { get; set; }

    public string? ParentName { get; set; }

    public string? Phone { get; set; }
    
    public string? Schedule { get; set; }
}