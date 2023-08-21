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
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepository, ITuitionRepository tuitionRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _tuitionRepository = tuitionRepository;
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

        var output = _mapper.Map<StudentInfoOutput>(student);
        return output;
    }

    public ApiResponseModel AddNewStudent(NewStudentInput input)
    {
        var newStudent = _mapper.Map<Student>(input);
        var addStuResult = _studentRepository.AddNewStudent(newStudent);

        var newTuition = _mapper.Map<Tuition>(input.Tuition);
        newTuition.StudentId = newStudent.Id;
        var addTuitionResult = _tuitionRepository.AddNewTuition(newTuition);

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
        existedStudent.Gender = input.Gender.Equals("Male");
        existedStudent.ParentName = input.ParentName;
        existedStudent.Phone = input.Phone;
        existedStudent.ModifiedAt = DateTime.Now;
        existedStudent.Schedule = input.Schedule;
        var updateStudentResult = _studentRepository.UpdateStudent(existedStudent);
        if (updateStudentResult == 0)
        {
            throw new Exception("Have some error while update student!");
        }

        return new ApiResponseModel
        {
            Code = StatusCodes.Status200OK,
            Message = "Update student information successfully!",
            Data = input,
            IsSuccess = true
        };
    }
}