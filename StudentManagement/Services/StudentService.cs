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

    public List<StudentOutput> GetAll()
    {
        var studentList = _studentRepository.GetAllStudents();
        var result = _mapper.Map<List<StudentOutput>>(studentList);
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

    public ApiResponseModel UpdateStudent(int studentId, UpdateStudentInput input)
    {
        var existedStudent = _studentRepository.GetStudentInfoByStudentId(studentId);
        if (existedStudent is null)
        {
            throw new Exception($"Student with id {studentId} is not exist!");
        }

        // update student
        existedStudent.FullName = input.FullName;
        existedStudent.Dob = input.Dob;
        existedStudent.Gender = input.Gender;
        existedStudent.ParentName = input.ParentName;
        existedStudent.Phone = input.Phone;
        existedStudent.ModifiedAt = DateTime.Now;
        var updateStudentResult = _studentRepository.UpdateStudent(existedStudent);
        if (updateStudentResult == 0)
        {
            throw new Exception("Have some error while update student!");
        }

        // update student timetables
        var studentTimetables = _studentRepository.GetStudentTimetablesByStudentId(studentId);
        List<StudentTimetable> newStuTimes;
        if (studentTimetables.Count > 0)
        {
            newStuTimes = input.Timetables
                .Select(timetable => new StudentTimetable
                {
                    StudentId = studentId,
                    TimeTableId = timetable.TimetableId,
                    CreatedAt = studentTimetables[0].CreatedAt,
                    ModifiedAt = DateTime.Now
                }).ToList();
        }
        else
        {
            newStuTimes = input.Timetables
                .Select(timetable => new StudentTimetable
                {
                    StudentId = studentId,
                    TimeTableId = timetable.TimetableId,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                }).ToList();
        }
        _studentRepository.DeleteStudentTimetables(studentTimetables);
        _studentRepository.AddStudentTimetables(newStuTimes);

        return new ApiResponseModel
        {
            Code = StatusCodes.Status200OK,
            Message = "Update student information successfully!",
            Data = input,
            IsSuccess = true
        };
    }
}