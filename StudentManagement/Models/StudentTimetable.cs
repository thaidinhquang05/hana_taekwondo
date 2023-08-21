using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class StudentTimetable
    {
        public int StudentId { get; set; }
        public int TimeTableId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual Student Student { get; set; } = null!;
        public virtual Timetable TimeTable { get; set; } = null!;
    }
}
