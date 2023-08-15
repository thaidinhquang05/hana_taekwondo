using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class StudentClass
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
