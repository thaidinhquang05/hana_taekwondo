using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface ITuitionRepository : IRepository<Tuition>
{
    int AddNewTuition(Tuition tuition);

    List<TuitionInfoOutput> GetTuitionByStudentId(int studentId);
}