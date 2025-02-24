namespace Application.Software.DTOs;

public class OrderSoftwareRequest
{
    public required string Id { get; set; }
    public required int Quantity { get; set; }
    public required int DurationMonths { get; set; }
}
