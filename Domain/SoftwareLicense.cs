namespace Domain;

public class SoftwareLicense
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string AccountId { get; set; }
    public required string SoftwareId { get; set; }
    public required string Name { get; set; }
    public required int Quantity { get; set; }
    public required LicenseState State { get; set; } = LicenseState.Active;
    public required DateTime ValidTo { get; set; }
}
