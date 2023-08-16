using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;
using StudentManagement.Utils.Interfaces;

namespace StudentManagement.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repo;
    private readonly ILogicHandler _logicHandler;

    public AuthService(IAuthRepository repo, ILogicHandler logicHandler)
    {
        _repo = repo;
        _logicHandler = logicHandler;
    }
    
    public ApiResponseModel Login(UserLogin model)
    {
        var user = _repo.GetUserByUsernameAndPassword(model.Username, model.Password);
        if (user is null)
        {
            throw new Exception("Username or password invalid!");
        }
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };
        var token = _logicHandler.GenerateJsonWebToken(claims);
        return new ApiResponseModel
        {
            Code = 200,
            Message = "Login successfully!",
            IsSuccess = true,
            Data = token
        };
    }
}