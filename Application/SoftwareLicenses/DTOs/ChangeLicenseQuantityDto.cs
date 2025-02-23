using Application.SoftwareLicenses.DTOs;

namespace Application.SoftwareServices.DTOs;

public class ChangeLicenseQuantityDto : PurchasedSoftwareLicenseDto
{
    public int Quantity { get; set; }
}
