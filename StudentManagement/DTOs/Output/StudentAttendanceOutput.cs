namespace StudentManagement.DTOs.Output
{
    public class StudentAttendanceOutput
    {
        public int Id { get; set; }

        public int Index { get; set; }

        public string? StudentImg { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public bool? IsAttend { get; set; }

        public string? Note { get; set; }
    }
}
