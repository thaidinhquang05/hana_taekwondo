using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;

namespace StudentManagement.Services.Interfaces;

public interface IStudentService
{
    List<StudentOutput> GetAll();

    StudentInfoOutput GetStudentInfo(int studentId);

    Task<ApiResponseModel> AddNewStudent(NewStudentInput input);

    Task<ApiResponseModel> UpdateStudent(int studentId, UpdateStudentInput input);

    void DeleteStudent(int studentId);
    
    List<StudentOutput> GetStudentByClass(int classId);
    
    public List<StudentOutput> GetStudentToAddClass(int classId);

}