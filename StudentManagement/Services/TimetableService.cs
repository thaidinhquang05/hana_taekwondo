using StudentManagement.DTOs.Output;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class TimetableService : ITimetableService
{
    private readonly ITimetableRepository _repository;

    public TimetableService(ITimetableRepository repository)
    {
        _repository = repository;
    }

    public List<TimetableOutput> GetTimetableByStudentId(int studentId)
    {
        var result = _repository.GetTimetableByStudentId(studentId);
        return result;
    }
}