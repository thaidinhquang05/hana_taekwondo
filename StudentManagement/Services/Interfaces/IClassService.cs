using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces
{
    public interface IClassService
    {
        ApiResponseModel AddNewStudentToClass(List<int> _studentIds, int _classId);

        ApiResponseModel RemoveStudentFromClass(int _studentId, int _classIds);

        List<ClassInfoOutput> GetAllClasses();

        ClassInfoOutput GetClassById(int classId);

        ApiResponseModel DeleteClass(int classId);

        ApiResponseModel AddNewClass(NewClassInput newClassInput);

        List<ClassByDateItem> GetClassesByDate(DateTime date);

        List<StudentAttendanceOutput> GetStudentByClassAndDate(int classId,DateTime date);

        ApiResponseModel TakeAttendance(int classId, DateTime date, List<StudentAttendanceInput> studentAttendanceInputs);
    }
}