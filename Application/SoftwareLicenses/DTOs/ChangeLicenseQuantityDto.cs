namespace Application.SoftwareServices.DTOs;

public class ChangeLicenseQuantityDto
{
    public required string Id { get; set; }
    public required string AccountId { get; set; }
    public required int Quantity { get; set; }
}
