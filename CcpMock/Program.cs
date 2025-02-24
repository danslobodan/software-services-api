var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/openapi/v1.json", "Cloud Computing Services Mock"));
}

app.MapControllers();
app.Run();