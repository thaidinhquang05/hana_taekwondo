using StudentManagement.DTOs.Output;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class TimetableService : ITimetableService
{
    private readonly ITimetableRepository _repository;
    private readonly IStudentRepository _studentRepository;

    public TimetableService(ITimetableRepository repository, IStudentRepository studentRepository)
    {
        _repository = repository;
        _studentRepository = studentRepository;
    }

    public List<TimetableOutput> GetAllTimetables()
    {
        var result = _repository.GetAllTimetables();
        return result;
    }

    public List<TimetableOutput> GetTimetableByStudentId(int studentId)
    {
        var existedStudent = _studentRepository.GetStudentInfoByStudentId(studentId);
        if (existedStudent is null)
        {
            throw new Exception($"Student with id: {studentId} is not exist!!!");
        }

        var result = _repository.GetTimetableByStudentId(studentId);
        return result;
    }
}