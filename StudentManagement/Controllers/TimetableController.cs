using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Output;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TimetableController : Controller
{
    private readonly ITimetableService _service;

    public TimetableController(ITimetableService service)
    {
        _service = service;
    }

    [HttpGet("{studentId:int}")]
    public ActionResult<ApiResponseModel> GetTimetablesByStudentId(int studentId)
    {
        try
        {
            var result = _service.GetTimetableByStudentId(studentId);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Student Information",
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