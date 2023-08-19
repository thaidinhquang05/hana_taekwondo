using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces
{
    public interface IClassService
    {
        public ApiResponseModel AddNewStudentToClass(int _studentId, int _classId);
        public ApiResponseModel RemoveStudentFromClass(Student _student);
    }
}
