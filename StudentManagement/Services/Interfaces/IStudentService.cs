using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces;

public interface IStudentService
{
    List<StudentOutput> GetAll();

    StudentInfoOutput GetStudentInfo(int studentId);

    ApiResponseModel AddNewStudent(NewStudentInput input);

    ApiResponseModel UpdateStudent(int studentId, UpdateStudentInput input);
}