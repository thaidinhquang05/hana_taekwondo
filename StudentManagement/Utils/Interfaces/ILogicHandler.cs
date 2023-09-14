using System.Security.Claims;

namespace StudentManagement.Utils.Interfaces;

public interface ILogicHandler
{
    public string GenerateJsonWebToken(Claim[] claims);

    public string? SaveImageFile(IFormFile imageFile, string? oldImage);

    public bool DeleteImg(string fileName);
}