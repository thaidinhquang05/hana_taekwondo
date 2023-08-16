using AutoMapper;
using StudentManagement.DTOs.Input;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<List<Student>> GetAll()
    {
        var result = _repository.GetAll();
        return result;
    }

    public Task AddNewStudent(NewStudentInput input)
    {
        _repository.Add(_mapper.Map<Student>(input));
        throw new NotImplementedException();
    }
}