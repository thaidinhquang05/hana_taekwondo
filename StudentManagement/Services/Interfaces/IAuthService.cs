using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;

namespace StudentManagement.Services.Interfaces;

public interface IAuthService
{
    ApiResponseModel Login(UserLogin model);
}