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

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApiDbContext>(opts => {
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    opts.UseNpgsql(connectionString);
});
builder.Services.AddStackExchangeRedisCache(opts => {
    opts.Configuration = builder.Configuration.GetConnectionString("Cache");
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
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/openapi/v1.json", "Software Services V1"));
}

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
