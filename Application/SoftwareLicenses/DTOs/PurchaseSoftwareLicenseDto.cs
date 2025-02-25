namespace Application.SoftwareLicenses.DTOs;

public class PurchaseSoftwareLicenseDto
{
    public string AccountId { get; set; } = string.Empty;
    public string SoftwareId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int DurationMonths { get; set; }
}
