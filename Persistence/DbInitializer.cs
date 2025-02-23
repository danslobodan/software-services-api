using Domain;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(ApiDbContext context)
    {
        if (context.Customers.Any())
            return;

        var customers = Enumerable.Range(0, 5)
            .Select(i => new Customer())
            .ToList();

        var accounts = customers
            .SelectMany(customer => {
                Console.WriteLine(customer.Id);
                var accounts = Enumerable.Range(0, 5)
                    .Select(i => new Account() {
                        CustomerId = customer.Id
                    });

                return accounts;
            })
            .ToList();

        var softwareLicenses = accounts
            .SelectMany(account => {
                var sofwareLicenses = Enumerable.Range(0, 5)
                    .Select(i => new SoftwareLicense() {
                        AccountId = account.Id,
                        SoftwareId = Guid.NewGuid().ToString(),
                        Name = $"Software Name {i}",
                        Quantity = 5,
                        State = LicenseState.Active,
                        ValidTo = DateTime.Now.AddYears(2)
                    });

                return sofwareLicenses;
            })
            .ToList();

        context.Customers.AddRange(customers);
        context.Accounts.AddRange(accounts);
        context.SoftwareLicenses.AddRange(softwareLicenses);
        
        await context.SaveChangesAsync();
    }
}
