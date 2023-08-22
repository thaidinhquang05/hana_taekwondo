using StudentManagement.Models;

namespace StudentManagement.DTOs.Input
{
    public class StudentClassInput
    {
        public List<int> StudentIds { get; set; }
        public int ClassId { get; set; }
    }
}
