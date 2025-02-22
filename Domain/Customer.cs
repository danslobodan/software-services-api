namespace Domain;

public class Customer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
