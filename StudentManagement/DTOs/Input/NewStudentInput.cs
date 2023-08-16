using StudentManagement.Models;

namespace StudentManagement.DTOs.Input;

public class NewStudentInput
{
    public string FullName { get; set; }

    public DateTime Dob { get; set; }

    public bool Gender { get; set; }

    public string? ParentName { get; set; }

    public string? Phone { get; set; }

    public List<TimetableInput> TimeTables { get; set; }
}