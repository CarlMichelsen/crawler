using Database;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .AddEnvironmentVariables();

// Dependency Injection
builder.Services
    .AddTransient<DataContext>();

builder.Services.AddHostedService<EsportalService>();

builder.Services.AddHealthChecks();
var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
//app.Environment.IsDevelopment()

// keep swagger in prod
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

//app.UseHttpsRedirection(); not ready for this yet

app.UseAuthorization();

app.MapControllers();

app.Run();