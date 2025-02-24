namespace Domain;

public class Account
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string CustomerId { get; set; }
    public ICollection<SoftwareLicense> SoftwareLicenses { get; set; } = [];
}
