using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class Slot
    {
        public Slot()
        {
            Attendances = new HashSet<Attendance>();
            Timetables = new HashSet<Timetable>();
        }

        public int Id { get; set; }
        public string? Desc { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Timetable> Timetables { get; set; }
    }
}
