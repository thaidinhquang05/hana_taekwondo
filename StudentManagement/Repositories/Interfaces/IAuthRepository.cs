using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces;

public interface IAuthRepository
{
    User GetUserByUsernameAndPassword(string username, string password);
}