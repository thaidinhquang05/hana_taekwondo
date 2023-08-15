using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class Tuition
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal ActualAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? Content { get; set; }
        public string? Note { get; set; }
    }
}
