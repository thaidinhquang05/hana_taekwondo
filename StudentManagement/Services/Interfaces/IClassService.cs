using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces
{
    public interface IClassService
    {
        public ApiResponseModel AddNewStudentToClass(List<int> _studentIds, int _classId);
        public ApiResponseModel RemoveStudentFromClass(Student _student);
        public List<ClassInfoOutput> GetAllClasses();
        public ClassInfoOutput GetClassById(int classId);
        public ApiResponseModel DeleteClass(int classId);
        public ApiResponseModel AddNewClass(NewClassInput newClassInput);
    }
}
