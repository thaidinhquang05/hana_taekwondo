using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs.Input;
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

    [HttpGet]
    public ActionResult<ApiResponseModel> GetListSpendingValue()
    {
        try
        {
            var result = _service.GetListSpending();
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

    [HttpGet("{spendingId:int}")]
    public ActionResult<ApiResponseModel> GetSpendingItem(int spendingId)
    {
        try
        {
            var result = _service.GetSpendingItem(spendingId);
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

    [HttpPost]
    public ActionResult<ApiResponseModel> AddSpending(SpendingInput input)
    {
        try
        {
            _service.AddNewSpending(input);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "New Spending Added Successfully!",
                IsSuccess = true,
                Data = input
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

    [HttpPut("{spendingId:int}")]
    public ActionResult<ApiResponseModel> UpdateSpending(int spendingId, [FromBody] SpendingInput input)
    {
        try
        {
            _service.UpdateSpending(spendingId, input);
            return Ok(new ApiResponseModel
            {
                Code = StatusCodes.Status200OK,
                Message = "Spending Updated Successfully!",
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

    [HttpDelete("{spendingId:int}")]
    public ActionResult<ApiResponseModel> DeleteSpendingRecord(int spendingId)
    {
        try
        {
            _service.DeleteSpendingRecord(spendingId);
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
}