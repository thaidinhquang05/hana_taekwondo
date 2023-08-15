using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Utils.Interfaces;

namespace StudentManagement.Utils;

public class LogicHandler : ILogicHandler
{
    private readonly IConfiguration _config;

    public LogicHandler(IConfiguration config)
    {
        _config = config;
    }
    
    public string GenerateJsonWebToken(Claim[] claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: credentials);

        var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodeToken;
    }
}