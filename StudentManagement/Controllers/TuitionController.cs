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

    [HttpGet("{tuitionId:int}")]
    public ActionResult<ApiResponseModel> GetTuitionById(int tuitionId)
    {
        try
        {
            var tuitionInfo = _service.GetTuitionById(tuitionId);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Tuition Information",
                IsSuccess = true,
                Data = tuitionInfo
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
    public ActionResult<ApiResponseModel> AddNewTuition(int studentId, [FromBody] TuitionInput input)
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

    [HttpPut("{tuitionId:int}")]
    public ActionResult<ApiResponseModel> UpdateTuitionInfo(int tuitionId, [FromBody] TuitionInput input)
    {
        try
        {
            _service.UpdateTuition(tuitionId, input);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Updated Successfully!",
                IsSuccess = true
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

    [HttpGet]
    public ActionResult<ApiResponseModel> GetDeadlineTutions()
    {
        try
        {
            var deadlineTutions = _service.DeadlineTution();
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Deadline Tution",
                IsSuccess = true,
                Data = deadlineTutions
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