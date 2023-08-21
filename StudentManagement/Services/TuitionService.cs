using StudentManagement.DTOs.Output;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class TuitionService : ITuitionService
{
    private readonly ITuitionRepository _repository;

    public TuitionService(ITuitionRepository repository)
    {
        _repository = repository;
    }
    
    public List<TuitionInfoOutput> GetTuitionByStudentId(int studentId)
    {
        var result = _repository.GetTuitionByStudentId(studentId);
        return result;
    }
}