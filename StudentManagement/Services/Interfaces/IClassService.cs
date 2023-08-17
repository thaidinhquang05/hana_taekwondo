using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces
{
    public interface IClassService
    {
        public ApiResponseModel AddNewStudentToClass(Student _student, Class _class);
    }
}
