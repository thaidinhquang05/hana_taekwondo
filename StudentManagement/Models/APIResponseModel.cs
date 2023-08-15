namespace StudentManagement.Models;

public class APIResponseModel
{
    public int Code { get; set; }

    public string Message { get; set; }

    public bool IsSuccess { get; set; }

    public object Data { get; set; }
}