using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Output;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SlotController : Controller
{
    private readonly ISlotService _service;

    public SlotController(ISlotService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<ApiResponseModel> GetSlots()
    {
        try
        {
            var result = _service.GetSlots();
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