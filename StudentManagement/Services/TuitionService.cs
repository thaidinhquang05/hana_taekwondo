using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class TuitionService : ITuitionService
{
    private readonly ITuitionRepository _repository;
    private readonly IMapper _mapper;

    public TuitionService(ITuitionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public List<TuitionInfoOutput> GetTuitionByStudentId(int studentId)
    {
        var result = _repository.GetTuitionByStudentId(studentId);
        return result;
    }

    public ApiResponseModel AddNewTuition(int studentId, NewTuitionInput input)
    {
        if (input.DueDate <= input.PaidDate)
        {
            throw new Exception("Due Date need to be greater than Paid Date!!!");
        }

        var newTuition = _mapper.Map<Tuition>(input);
        newTuition.StudentId = studentId;
        var addTuitionResult = _repository.AddNewTuition(newTuition);
        if (addTuitionResult == 0)
        {
            throw new Exception("Have something wrong when adding tuition!");
        }

        return new ApiResponseModel
        {
            Code = StatusCodes.Status200OK,
            Message = "Student Information",
            IsSuccess = true,
            Data = input
        };
    }
}