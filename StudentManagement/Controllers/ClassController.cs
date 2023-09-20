using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ClassController : Controller
{
    private readonly IClassService _classService;

    public ClassController(IClassService classService)
    {
        _classService = classService;
    }

    [HttpGet]
    public ActionResult<ApiResponseModel> GetAllClasses()
    {
        try
        {
            var classes = _classService.GetAllClasses();
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Get All Classes Success!",
                Data = classes,
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

    [HttpPost]
    public ActionResult<ApiResponseModel> AddStudentToClass([FromBody] StudentClassInput studentClass)
    {
        try
        {
            var result = _classService.AddNewStudentToClass(studentClass.StudentIds, studentClass.ClassId);
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

    [HttpGet("{classId:int}")]
    public ActionResult<ApiResponseModel> GetClassById(int classId)
    {
        try
        {
            var _class = _classService.GetClassById(classId);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Get Class Success!",
                Data = _class,
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

    [HttpDelete("{classId:int}")]
    public ActionResult<ApiResponseModel> DeleteClass(int classId)
    {
        try
        {
            var result = _classService.DeleteClass(classId);
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

    [HttpDelete("{studentId:int},{classId:int}")]
    public ActionResult<ApiResponseModel> RemoveStudent(int studentId, int classId)
    {
        try
        {
            var result = _classService.RemoveStudentFromClass(studentId, classId);
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

    [HttpPost]
    public ActionResult<ApiResponseModel> AddNewClass([FromBody] NewClassInput newClassInput)
    {
        try
        {
            var result = _classService.AddNewClass(newClassInput);
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

    [HttpGet]
    public ActionResult<ApiResponseModel> GetClassesByDate(DateTime date)
    {
        try
        {
            var result = _classService.GetClassesByDate(date);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Get Classes successfully",
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

    [HttpGet]
    public ActionResult<ApiResponseModel> GetStudentByClassAndDate(int classId, DateTime date)
    {
        try
        {
            var result = _classService.GetStudentByClassAndDate(classId, date);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Get Students successfully",
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

    [HttpPost]
    public ActionResult<ApiResponseModel> TakeAttendance(int classId, DateTime date, List<StudentAttendanceInput> studentAttendanceInputs)
    {
        try
        {
            var result = _classService.TakeAttendance(classId, date, studentAttendanceInputs);
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