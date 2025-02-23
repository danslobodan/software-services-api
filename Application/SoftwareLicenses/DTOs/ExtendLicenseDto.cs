using System;

namespace Application.SoftwareLicenses.DTOs;

public class ExtendLicenseDto
{
    public required string Id { get; set; }
    public required string AccountId { get; set; }
    public required int Months { get; set; }
}
