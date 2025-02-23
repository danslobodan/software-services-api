namespace Application.SoftwareServices.DTOs;

public class PurchaseSoftwareLicenseDto
{
    public required string Id { get; set; } = Guid.NewGuid().ToString();
    public required string AccountId { get; set; }
    public required string SoftwareId { get; set; }
    public required string Name { get; set; }
    public required int Quantity { get; set; }
    public required int DurationMonths { get; set; }
}
