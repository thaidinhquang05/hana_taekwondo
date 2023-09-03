namespace StudentManagement.DTOs.Output;

public class SpendingItemValueOutput
{
    public int Month { get; set; }

    public decimal ElectricSpending { get; set; }

    public decimal WaterSpending { get; set; }

    public decimal RentSpending { get; set; }

    public decimal AnotherSpending { get; set; }

    public decimal Total { get; set; }

    public string PaidDate { get; set; }
}