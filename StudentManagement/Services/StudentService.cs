﻿using AutoMapper;
using Newtonsoft.Json;
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

    public async Task<ApiResponseModel> AddNewStudent(NewStudentInput input)
    {
        using var memoryStream = new MemoryStream();
        await input.StudentImg.CopyToAsync(memoryStream);
        var imageBytes = memoryStream.ToArray();
        var base64String = Convert.ToBase64String(imageBytes);

        var newStudent = _mapper.Map<Student>(input);
        newStudent.StudentImg = $"data:image/jpg;base64,{base64String}";
        _studentRepository.AddNewStudent(newStudent);

        if (input.Tuition is not null)
        {
            var tuition = JsonConvert.DeserializeObject<TuitionInput>(input.Tuition);
            if (tuition is not null)
            {
                if (tuition.DueDate <= tuition.PaidDate)
                {
                    throw new Exception("Due Date need to be greater than Paid Date!!!");
                }

                var newTuition = _mapper.Map<Tuition>(tuition);
                newTuition.StudentId = newStudent.Id;
                _tuitionRepository.AddNewTuition(newTuition);
            }
        }

        if (input.Timetables is not null)
        {
            var timetables = JsonConvert.DeserializeObject<List<TimetableInput>>(input.Timetables);
            if (timetables is not null)
            {
                var newStuTimes = timetables
                    .Select(timetable => new StudentTimetable
                    {
                        StudentId = newStudent.Id,
                        TimeTableId = timetable.TimetableId,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    }).ToList();
                _studentRepository.AddStudentTimetables(newStuTimes);
            }
        }

        return new ApiResponseModel
        {
            Code = StatusCodes.Status200OK,
            Message = "Add new student successfully!",
            Data = input,
            IsSuccess = true
        };
    }

    public async Task<ApiResponseModel> UpdateStudent(int studentId, UpdateStudentInput input)
    {
        var existedStudent = _studentRepository.GetStudentInfoByStudentId(studentId);
        if (existedStudent is null)
        {
            throw new Exception($"Student with id {studentId} is not exist!");
        }

        // update student
        if (input.StudentImg is null || input.StudentImg.Length == 0)
        {
            throw new Exception("Invalid File!");
        }

        using var memoryStream = new MemoryStream();
        await input.StudentImg.CopyToAsync(memoryStream);
        var imageBytes = memoryStream.ToArray();
        var base64String = Convert.ToBase64String(imageBytes);

        existedStudent.StudentImg = $"data:image/jpg;base64,{base64String}";
        existedStudent.FullName = input.FullName;
        existedStudent.Dob = input.Dob;
        existedStudent.Gender = input.Gender.Equals("Male");
        existedStudent.ParentName = input.ParentName;
        existedStudent.Phone = input.Phone;
        existedStudent.ModifiedAt = DateTime.Now;
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

    public void DeleteStudent(int studentId)
    {
        var student = _studentRepository.GetStudentInfoByStudentId(studentId);
        if (student is null)
        {
            throw new Exception("Student does not exist!!!");
        }

        var studentClasses = _studentRepository.GetStudentClassesByStudentId(studentId);
        _studentRepository.DeleteStudentClass(studentClasses);
        var studentTimetables = _studentRepository.GetStudentTimetablesByStudentId(studentId);
        _studentRepository.DeleteStudentTimetables(studentTimetables);
        var tuition = _tuitionRepository.GetTuitionByStudentId(studentId);
        _tuitionRepository.DeleteTuition(tuition);
        _studentRepository.Delete(student);
    }

    public List<StudentOutput> GetStudentByClass(int classId)
    {
        var studentList = _studentRepository.GetStudentByClass(classId);
        var result = _mapper.Map<List<StudentOutput>>(studentList);
        return result;
    }

    public List<StudentOutput> GetStudentToAddClass(int classId)
    {
        var studentList = _studentRepository.GetStudentToAddClass(classId);
        var result = _mapper.Map<List<StudentOutput>>(studentList);
        return result;
    }
}