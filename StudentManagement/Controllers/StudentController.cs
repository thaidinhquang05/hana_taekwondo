using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Services.Interfaces;

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
    public ActionResult<ApiResponseModel> GetAllStudents()
    {
        try
        {
            var students = _studentService.GetAll();
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
    public ActionResult<ApiResponseModel> AddNewStudent([FromForm] NewStudentInput input)
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

    [HttpPut("{studentId:int}")]
    public async Task<ActionResult<ApiResponseModel>> UpdateStudent([FromForm] UpdateStudentInput input, int studentId)
    {
        try
        {
            var result = await _studentService.UpdateStudent(studentId, input);
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

    [HttpDelete("{studentId:int}")]
    public ActionResult<ApiResponseModel> DeleteStudent(int studentId)
    {
        try
        {
            _studentService.DeleteStudent(studentId);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Deleted Successfully!",
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

    [HttpGet("{classId:int}")]
    public ActionResult<ApiResponseModel> GetStudentsByClass(int classId)
    {
        try
        {
            var student = _studentService.GetStudentByClass(classId);

            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Student Information",
                IsSuccess = true,
                Data = student
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

    [HttpGet("{classId:int}")]
    public ActionResult<ApiResponseModel> GetStudentToAddClass(int classId)
    {
        try
        {
            var studentInfo = _studentService.GetStudentToAddClass(classId);

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
}