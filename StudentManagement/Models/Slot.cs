using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class Slot
    {
        public Slot()
        {
            Timetables = new HashSet<Timetable>();
        }

        public int Id { get; set; }
        public string? Desc { get; set; }

        public virtual ICollection<Timetable> Timetables { get; set; }
    }
}
