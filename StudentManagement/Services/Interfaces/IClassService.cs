using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces
{
    public interface IClassService
    {
        public ApiResponseModel AddNewStudentToClass(List<int> _studentIds, int _classId);
        public ApiResponseModel RemoveStudentFromClass(Student _student);
        public List<ClassInfoOutput> GetAllClasses();
    }
}
