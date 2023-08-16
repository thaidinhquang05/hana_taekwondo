using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public ActionResult<ApiResponseModel> Login([FromBody] UserLogin model)
    {
        try
        {
            var result = _authService.Login(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Conflict(new ApiResponseModel
            {
                Code = StatusCodes.Status400BadRequest,
                Message = ex.Message,
                IsSuccess = false
            });
        }
    }
}