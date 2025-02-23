namespace Application.SoftwareLicenses.DTOs;

public class ExtendLicenseDto : PurchasedSoftwareLicenseDto
{
    public int DurationMonths { get; set; }
}
