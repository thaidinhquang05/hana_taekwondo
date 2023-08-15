using System.Security.Claims;

namespace StudentManagement.Utils.Interfaces;

public interface ILogicHandler
{
    public string GenerateJsonWebToken(Claim[] claims);
}