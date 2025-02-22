namespace Domain;

public class SoftwareService
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public required int Quantity { get; set; }
    public required LicenseState State { get; set; }
    public required DateTime ValidTo { get; set; }
}
