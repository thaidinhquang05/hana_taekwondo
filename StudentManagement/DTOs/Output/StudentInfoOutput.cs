namespace StudentManagement.DTOs.Output;

public class StudentInfoOutput
{
    public int Id { get; set; }
    
    public string FullName { get; set; }
    
    public DateTime Dob { get; set; }
    
    public bool Gender { get; set; }
    
    public string? ParentName { get; set; }
    
    public string? Phone { get; set; }
    
    public List<TuitionInfoOutput> Tuition { get; set; }
    
    public List<ClassInfoOutput> Class { get; set; }

    public List<TimetableOutput> Timetable { get; set; }
}