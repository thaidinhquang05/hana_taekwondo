using StudentManagement.DTOs.Input;
using StudentManagement.Models;

namespace StudentManagement.Services.Interfaces;

public interface IAuthService
{
    APIResponseModel Login(UserLogin model);
}