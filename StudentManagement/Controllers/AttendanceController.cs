using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Output;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AttendanceController : Controller
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IStudentRepository _studentRepository;

    public AttendanceController(IAttendanceRepository attendanceRepository, IStudentRepository studentRepository)
    {
        _attendanceRepository = attendanceRepository;
        _studentRepository = studentRepository;
    }

    [HttpGet("{studentId:int}")]
    public ActionResult<ApiResponseModel> GetAttendanceByStudentId(int studentId)
    {
        try
        {
            var student = _studentRepository.GetStudentInfoByStudentId(studentId);
            if (student is null)
            {
                return NotFound(new ApiResponseModel
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "Student not found",
                    IsSuccess = false,
                });
            }
            
            var result = _attendanceRepository
                .GetAttendanceByStudentId(studentId)
                .Select((x, index) => new
                {
                    Index = index + 1,
                    Id = x.Id,
                    StudentId = x.StudentId,
                    StudentName = x.Student.FullName,
                    SlotDesc = x.Slot.Desc,
                    IsAttendance = x.IsAttendance,
                    Note = x.Note,
                    Date = $"{x.Date:yyyy-MM-dd}"
                })
                .ToList();
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Successfully",
                IsSuccess = true,
                Data = result
            });
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