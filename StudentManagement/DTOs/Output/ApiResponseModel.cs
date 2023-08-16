namespace StudentManagement.DTOs.Output;

public class ApiResponseModel
{
    public int Code { get; set; }

    public string Message { get; set; }

    public bool IsSuccess { get; set; }

    public object Data { get; set; }
}