using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class Spending
    {
        public int Id { get; set; }
        public decimal Electric { get; set; }
        public decimal Water { get; set; }
        public decimal Rent { get; set; }
        public decimal Another { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime PaidDate { get; set; }
        public string? Content { get; set; }
        public decimal Salary { get; set; }
        public decimal Eating { get; set; }
    }
}
