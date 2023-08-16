using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly hana_taekwondoContext _context;

    public AuthRepository(hana_taekwondoContext context)
    {
        _context = context;
    }
    
    public User GetUserByUsernameAndPassword(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(username) && u.Password.Equals(password));
        return user;
    }
}