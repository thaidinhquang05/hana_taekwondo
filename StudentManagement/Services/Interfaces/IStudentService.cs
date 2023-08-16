using StudentManagement.DTOs.Input;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces;

public interface IStudentService
{
    Task<List<Student>> GetAll();

    Task AddNewStudent(NewStudentInput input);
}