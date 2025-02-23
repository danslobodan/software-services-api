using API.Middleware;
using Application.Accounts.Queries;
using Application.Core;
using Application.SoftwareServices.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApiDbContext>(opts => {
    opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMediatR(x => {
    x.RegisterServicesFromAssemblyContaining<GetAccountsList.Handler>();
    x.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<PurchaseSoftwareLicenseValidator>();
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try {
    var context = services.GetRequiredService<ApiDbContext>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
} catch (Exception ex) {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}

app.Run();
