using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Input;
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

    [HttpGet]
    public ActionResult<ApiResponseModel> GetAllTimetables()
    {
        try
        {
            var result = _service.GetAllTimetables();
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Timetables Information!",
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

    [HttpGet("{studentId:int}")]
    public ActionResult<ApiResponseModel> GetTimetablesByStudentId(int studentId)
    {
        try
        {
            var result = _service.GetTimetableByStudentId(studentId);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Student's Timetable Information!",
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

    [HttpPut("{studentId:int}")]
    public ActionResult<ApiResponseModel> UpdateStudentTimetables(int studentId, [FromBody] List<TimetableInput> input)
    {
        try
        {
            _service.UpdateStudentTimetables(studentId, input);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Timetables Updated Successfully!",
                IsSuccess = true,
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