using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using System;

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

        List<StudentAttendanceOutput> GetStudentBySlotAndDate(int slotId, DateTime date, string daysOfWeek);

        ApiResponseModel TakeAttendance(int slotId, DateTime date, List<StudentAttendanceInput> studentAttendanceInputs);
        List<StudentAttendanceOutput> GetStudentMakeUpBySlotAndDate(DateTime date);

        ApiResponseModel TakeMakeUpAttendance(int slotId, DateTime date, List<StudentAttendanceInput> studentAttendanceInputs);
    }
}