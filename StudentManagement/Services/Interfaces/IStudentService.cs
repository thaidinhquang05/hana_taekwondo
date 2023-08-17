using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces;

public interface IStudentService
{
    Task<List<Student>> GetAll();

    StudentInfoOutput GetStudentInfo(int studentId);

    ApiResponseModel AddNewStudent(NewStudentInput input);
}