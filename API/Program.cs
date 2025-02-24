using API.Middleware;
using Application.Accounts.Queries;
using Application.Core;
using Application.Interfaces;
using Application.SoftwareServices.Validators;
using FluentValidation;
using Infrastructure.SoftwareVendors;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApiDbContext>(opts => {
    opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string not found."));
});
builder.Services.AddMediatR(x => {
    x.RegisterServicesFromAssemblyContaining<GetAccountsList.Handler>();
    x.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});

builder.Services.AddHttpClient("ccp", client => {
    var baseUrl = builder.Configuration.GetSection("CcpSettings")
        .GetValue<string>("BaseUrl") ?? string.Empty;
    client.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddScoped<ISoftwareVendor, CcpService>();
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

    if (app.Environment.IsDevelopment())
        await DbInitializer.SeedData(context);
} catch (Exception ex) {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}

app.Run();
