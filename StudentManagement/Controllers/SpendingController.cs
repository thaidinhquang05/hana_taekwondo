using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Output;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SpendingController : Controller
{
    private readonly ISpendingService _service;

    public SpendingController(ISpendingService service)
    {
        _service = service;
    }

    [HttpGet("{month:int}/{year:int}")]
    public ActionResult<ApiResponseModel> GetSpendingValue(int month, int year)
    {
        try
        {
            var result = _service.GetSpendingValue(month, year);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Success!",
                IsSuccess = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            return Conflict(new ApiResponseModel
            {
                Code = StatusCodes.Status409Conflict,
                Message = ex.Message,
                IsSuccess = false
            });
        }
    }
}