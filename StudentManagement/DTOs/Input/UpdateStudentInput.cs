namespace StudentManagement.DTOs.Input;

public class UpdateStudentInput
{
    public string FullName { get; set; }

    public DateTime Dob { get; set; }

    public string Gender { get; set; }

    public string? ParentName { get; set; }

    public string? Phone { get; set; }
    
    public string? Schedule { get; set; }
}