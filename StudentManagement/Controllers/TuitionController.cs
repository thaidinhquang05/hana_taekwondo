using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TuitionController : Controller
{
    private readonly ITuitionService _service;

    public TuitionController(ITuitionService service)
    {
        _service = service;
    }

    [HttpGet("{studentId:int}")]
    public ActionResult<ApiResponseModel> GetTuitionByStudentId(int studentId)
    {
        try
        {
            var result = _service.GetTuitionByStudentId(studentId);
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

    [HttpPost("{studentId:int}")]
    public ActionResult<ApiResponseModel> AddNewTuition(int studentId, [FromBody] NewTuitionInput input)
    {
        try
        {
            var result = _service.AddNewTuition(studentId, input);
            return Ok(result);
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