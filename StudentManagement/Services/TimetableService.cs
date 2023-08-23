using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
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

    public List<StudentTimetableOutput> GetTimetableByStudentId(int studentId)
    {
        var existedStudent = _studentRepository.GetStudentInfoByStudentId(studentId);
        if (existedStudent is null)
        {
            throw new Exception($"Student with id: {studentId} is not exist!!!");
        }

        var result = _repository.GetTimetableByStudentId(studentId);
        return result;
    }

    public void UpdateStudentTimetables(int studentId, List<TimetableInput> input)
    {
        var existedStudent = _studentRepository.GetStudentInfoByStudentId(studentId);
        if (existedStudent is null)
        {
            throw new Exception($"Student with id: {studentId} is not exist!!!");
        }

        _repository.RemoveStudentTimetables(studentId);

        if (input.Count <= 0) return;
        var newStuTimes = input
            .Select(item => new StudentTimetable
            {
                StudentId = studentId,
                TimeTableId = item.TimetableId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            }).ToList();
        _repository.AddStudentTimetables(newStuTimes);
    }
}