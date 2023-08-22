using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DTOs.Input;

public class NewStudentInput
{
    [Required(ErrorMessage = "Name must be filled!")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Birth must be filled!")]
    public DateTime Dob { get; set; }

    [Required(ErrorMessage = "Gender must be filled!")]
    public bool Gender { get; set; }

    public string? ParentName { get; set; }

    public string? Phone { get; set; }

    public NewTuitionInput? Tuition { get; set; }

    public List<TimetableInput>? Timetables { get; set; }
}