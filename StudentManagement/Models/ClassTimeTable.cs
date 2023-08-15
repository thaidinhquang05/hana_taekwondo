using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class ClassTimeTable
    {
        public int ClassId { get; set; }
        public int TimeTableId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
