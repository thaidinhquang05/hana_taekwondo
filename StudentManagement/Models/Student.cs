using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
            StudentClasses = new HashSet<StudentClass>();
            StudentTimetables = new HashSet<StudentTimetable>();
            Tuitions = new HashSet<Tuition>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public string? ParentName { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? StudentImg { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        public virtual ICollection<StudentTimetable> StudentTimetables { get; set; }
        public virtual ICollection<Tuition> Tuitions { get; set; }
    }
}
