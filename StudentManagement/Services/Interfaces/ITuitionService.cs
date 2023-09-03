using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;

namespace StudentManagement.Services.Interfaces;

public interface ITuitionService
{
    List<TuitionInfoOutput> GetTuitionByStudentId(int studentId);

    TuitionInfoOutput GetTuitionById(int tuitionId);

    ApiResponseModel AddNewTuition(int studentId, TuitionInput input);

    void UpdateTuition(int tuitionId, TuitionInput input);

    EarningValueOutput GetEarningValueByMonth(int month, int year);
}