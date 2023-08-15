using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class TimeTable
    {
        public TimeTable()
        {
            StudentTimeTables = new HashSet<StudentTimeTable>();
        }

        public int Id { get; set; }
        public string WeekDay { get; set; } = null!;
        public int Slot { get; set; }

        public virtual ICollection<StudentTimeTable> StudentTimeTables { get; set; }
    }
}
