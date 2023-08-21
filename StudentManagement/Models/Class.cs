using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class Class
    {
        public Class()
        {
            StudentClasses = new HashSet<StudentClass>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Desc { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Schedule { get; set; }

        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}
