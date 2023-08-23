using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DTOs.Input
{
    public class NewClassInput
    {
        [Required(ErrorMessage = "Name must be filled!")]
        public string Name { get; set; } = null!;
        public string? Desc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
