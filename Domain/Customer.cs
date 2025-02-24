namespace Domain;

public class Customer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public ICollection<Account> Accounts { get; set; } = [];
}
