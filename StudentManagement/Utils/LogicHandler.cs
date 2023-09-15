using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Utils.Interfaces;

namespace StudentManagement.Utils;

public class LogicHandler : ILogicHandler
{
    // private const string TargetFolder = @"..\httpdocs\img\student";
    private const string TargetFolder = @"..\..\StudentManagement\Hana_FE\img\student";
    
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

    public string? SaveImageFile(IFormFile imageFile, string? oldImage)
    {
        try
        {
            var extension = Path.GetExtension(imageFile.FileName);
            if (!extension.ToLower().Equals(".jpg") && !extension.ToLower().Equals(".jpeg") &&
                !extension.ToLower().Equals(".png"))
            {
                return null;
            }

            if (!Directory.Exists(TargetFolder))
            {
                Directory.CreateDirectory(TargetFolder);
            }

            var r = new Random();
            var imageNameFile = r.Next() + imageFile.FileName;
            var newImagePath = Path.Combine(TargetFolder, imageNameFile);

            using var stream = new FileStream(newImagePath, FileMode.Create);
            imageFile.CopyTo(stream);

            if (oldImage != null)
            {
                var oldImgPath = Path.Combine(TargetFolder, oldImage);
                var fileDelete = new FileInfo(oldImgPath);
                if (fileDelete.Length > 0)
                {
                    File.Delete(oldImgPath);
                    fileDelete.Delete();
                }
            }

            return imageNameFile;
        }
        catch
        {
            return null;
        }
    }

    public bool DeleteImg(string fileName)
    {
        try
        {
            if (string.IsNullOrEmpty(fileName)) return false;

            var oldImgPath = Path.Combine(TargetFolder, fileName);
            var fileDelete = new FileInfo(oldImgPath);

            if (fileDelete.Length <= 0) return false;

            File.Delete(oldImgPath);
            fileDelete.Delete();
            return true;
        }
        catch
        {
            return false;
        }
    }
}