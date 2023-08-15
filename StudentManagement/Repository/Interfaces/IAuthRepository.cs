using StudentManagement.Models;

namespace StudentManagement.Repository.Interfaces;

public interface IAuthRepository
{
    User GetUserByUsernameAndPassword(string username, string password);
}