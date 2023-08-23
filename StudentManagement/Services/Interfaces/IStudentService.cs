using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;

namespace StudentManagement.Services.Interfaces;

public interface IStudentService
{
    List<StudentOutput> GetAll();

    StudentInfoOutput GetStudentInfo(int studentId);

    ApiResponseModel AddNewStudent(NewStudentInput input);

    ApiResponseModel UpdateStudent(int studentId, UpdateStudentInput input);
    
    List<StudentOutput> GetStudentByClass(int classId);
    
    public List<StudentOutput> GetStudentToAddClass(int classId);

}