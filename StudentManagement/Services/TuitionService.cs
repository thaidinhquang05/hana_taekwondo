using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;
using StudentManagement.Repositories;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class TuitionService : ITuitionService
{
    private readonly ITuitionRepository _repository;
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public TuitionService(ITuitionRepository repository, IStudentRepository studentRepository, IMapper mapper)
    {
        _repository = repository;
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public List<TuitionInfoOutput> GetTuitionByStudentId(int studentId)
    {
        var response = _repository.GetTuitionByStudentId(studentId);
        var result = response.Select((x, i) => new TuitionInfoOutput
        {
            Id = x.Id,
            Index = i + 1,
            PaidDate = $"{x.PaidDate:yyyy-MM-dd}",
            DueDate = $"{x.DueDate:yyyy-MM-dd}",
            Amount = x.Amount,
            ActualAmount = x.ActualAmount,
            Content = x.Content,
            Note = x.Note
        }).ToList();
        return result;
    }

    public TuitionInfoOutput GetTuitionById(int tuitionId)
    {
        var tuitionInfo = _repository.GetTuitionById(tuitionId);
        if (tuitionInfo is null)
        {
            throw new Exception("Record not found!!!");
        }

        return new TuitionInfoOutput
        {
            Id = tuitionInfo.Id,
            PaidDate = $"{tuitionInfo.PaidDate:yyyy-MM-dd}",
            DueDate = $"{tuitionInfo.DueDate:yyyy-MM-dd}",
            Amount = tuitionInfo.Amount,
            ActualAmount = tuitionInfo.ActualAmount,
            Content = tuitionInfo.Content,
            Note = tuitionInfo.Note
        };
    }

    public ApiResponseModel AddNewTuition(int studentId, TuitionInput input)
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

    public void UpdateTuition(int tuitionId, TuitionInput input)
    {
        var tuition = _repository.GetTuitionById(tuitionId);
        if (tuition is null)
        {
            throw new Exception("This record is not exist!!!");
        }

        tuition.PaidDate = input.PaidDate;
        tuition.DueDate = input.DueDate;
        tuition.ActualAmount = input.ActualAmount;
        tuition.Amount = input.Amount;
        tuition.Content = input.Content;
        tuition.Note = input.Note;
        _repository.Update(tuition);
    }

    public void DeleteTuitionRecord(int tuitionId)
    {
        var tuition = _repository.GetTuitionById(tuitionId);
        if (tuition is null)
        {
            throw new Exception("This record is not exist!!!");
        }

        _repository.Delete(tuition);
    }

    public EarningValueOutput GetEarningValueByMonth(int month, int year)
    {
        var earningValue = _repository.GetEarningValueByMonth(month, year);
        return earningValue;
    }

    public List<DeadlineTutionOutput> DeadlineTution()
    {
        try
        {
            List<DeadlineTutionOutput> deadlineTutionOutputs = new List<DeadlineTutionOutput>();
            List<Student> students = _studentRepository.GetUpcomingDeadlinesStudent();

            foreach (var item in students)
            {
                DeadlineTutionOutput deadlineTutionOutput = new();
                deadlineTutionOutput.FullName = item.FullName;
                deadlineTutionOutput.StudentId = item.Id;
                deadlineTutionOutput.StudentImg = item.StudentImg;
                deadlineTutionOutput.DueDate = _repository.GetTuitionDeadlineByStudentId(item.Id).DueDate.ToShortDateString();
                deadlineTutionOutput.NotificationTime = _repository.GetTuitionDeadlineByStudentId(item.Id).DueDate.AddDays(-5);
                deadlineTutionOutputs.Add(deadlineTutionOutput);
            }

            return deadlineTutionOutputs;
        }
        catch
        {
            throw new Exception("Not Found Any Record!");
        }
    }
}