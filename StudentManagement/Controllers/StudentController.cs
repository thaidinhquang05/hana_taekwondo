using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Input;
using StudentManagement.Services.Interfaces;
using StudentManagement.DTOs.Output;

namespace StudentManagement.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class StudentController : Controller
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponseModel>> GetAllStudents()
    {
        try
        {
            var students = await _studentService.GetAll();
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Get All Students Success!",
                Data = students,
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

    [HttpGet("{studentId:int}")]
    public ActionResult<ApiResponseModel> GetStudentInfo(int studentId)
    {
        try
        {
            var studentInfo = _studentService.GetStudentInfo(studentId);
            
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Student Information",
                IsSuccess = true,
                Data = studentInfo
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

    [HttpPost]
    public ActionResult<ApiResponseModel> AddNewStudent([FromBody] NewStudentInput input)
    {
        try
        {
            var response = _studentService.AddNewStudent(input);
            return Ok(response);
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