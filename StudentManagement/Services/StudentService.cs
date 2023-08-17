using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ITuitionRepository _tuitionRepository;
    private readonly IClassRepository _classRepository;
    private readonly ITimetableRepository _timetableRepository;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepository, ITuitionRepository tuitionRepository,
        IClassRepository classRepository, ITimetableRepository timetableRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _tuitionRepository = tuitionRepository;
        _classRepository = classRepository;
        _timetableRepository = timetableRepository;
        _mapper = mapper;
    }

    public Task<List<Student>> GetAll()
    {
        var result = _studentRepository.GetAll();
        return result;
    }

    public StudentInfoOutput GetStudentInfo(int studentId)
    {
        var student = _studentRepository.GetStudentInfoByStudentId(studentId);
        if (student is null)
        {
            throw new Exception($"Student with id {studentId} is not exist!");
        }

        var studentClass = _classRepository.GetClassesByStudentId(studentId);
        var studentTuition = _tuitionRepository.GetTuitionByStudentId(studentId);
        var studentTimetable = _timetableRepository.GetTimetableByStudentId(studentId);

        var output = _mapper.Map<StudentInfoOutput>(student);
        output.Class = studentClass;
        output.Tuition = studentTuition;
        output.Timetable = studentTimetable;

        return output;
    }

    public ApiResponseModel AddNewStudent(NewStudentInput input)
    {
        var newStudent = _mapper.Map<Student>(input);
        var addStuResult = _studentRepository.AddNewStudent(newStudent);

        var newTuition = _mapper.Map<Tuition>(input.Tuition);
        newTuition.StudentId = newStudent.Id;
        var addTuitionResult = _tuitionRepository.AddNewTuition(newTuition);

        var newStuTimes = input.Timetables
            .Select(timetable => new StudentTimetable
            {
                StudentId = newStudent.Id,
                TimeTableId = timetable.TimetableId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            }).ToList();

        _studentRepository.AddStudentTimetables(newStuTimes);

        if (addStuResult == 0 || addTuitionResult == 0)
        {
            throw new Exception("Have something wrong when add new student!");
        }

        return new ApiResponseModel
        {
            Code = StatusCodes.Status200OK,
            Message = "Add new student successfully!",
            Data = input,
            IsSuccess = true
        };
    }
}