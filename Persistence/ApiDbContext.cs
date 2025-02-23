using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApiDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<SoftwareLicense> SoftwareLicenses { get; set; }
}
