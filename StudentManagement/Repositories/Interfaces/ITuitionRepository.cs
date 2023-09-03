using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface ITuitionRepository : IRepository<Tuition>
{
    int AddNewTuition(Tuition tuition);

    List<Tuition> GetTuitionByStudentId(int studentId);

    void DeleteTuition(List<Tuition> entities);

    Tuition GetTuitionById(int tuitionId);

    EarningValueOutput GetEarningValueByMonth(int month, int year);
}