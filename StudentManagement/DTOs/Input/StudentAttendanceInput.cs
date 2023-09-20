namespace StudentManagement.DTOs.Input
{
    public class StudentAttendanceInput
    {
        public int Id { get; set; }

        public bool IsAttend { get; set; }

        public string? Note { get; set; }
    }
}
