namespace StudentManagement.DTOs.Input;

public class UpdateStudentInput
{
    public string FullName { get; set; }

    public DateTime Dob { get; set; }

    public bool Gender { get; set; }

    public string? ParentName { get; set; }

    public string? Phone { get; set; }

    public List<TimetableInput> Timetables { get; set; }
}