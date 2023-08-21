using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class Timetable
    {
        public Timetable()
        {
            ClassTimetables = new HashSet<ClassTimetable>();
            StudentTimetables = new HashSet<StudentTimetable>();
        }

        public int Id { get; set; }
        public string WeekDay { get; set; } = null!;
        public int SlotId { get; set; }

        public virtual Slot Slot { get; set; } = null!;
        public virtual ICollection<ClassTimetable> ClassTimetables { get; set; }
        public virtual ICollection<StudentTimetable> StudentTimetables { get; set; }
    }
}
