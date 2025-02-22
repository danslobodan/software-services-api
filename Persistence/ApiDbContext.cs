using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApiDbContext(DbContextOptions options) : DbContext(options)
{
}
