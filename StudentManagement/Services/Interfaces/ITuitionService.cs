using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;

namespace StudentManagement.Services.Interfaces;

public interface ITuitionService
{
    List<TuitionInfoOutput> GetTuitionByStudentId(int studentId);

    ApiResponseModel AddNewTuition(int studentId, NewTuitionInput input);
}