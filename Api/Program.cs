using Database;
using Services;
using WebCrawler.Esportal;
using WebCrawler.Esportal.Services;

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
    .AddTransient<EsportalCrawler>()
    .AddTransient<EsportalSteamIdCrawler>()
    .AddTransient<EsportalProfileService>();

builder.Services.AddDbContext<DataContext>();

//builder.Services.AddHostedService<EsportalService>();
//builder.Services.AddHostedService<EsportalSteamIdService>();

builder.Services.AddHttpClient<EsportalService>();

builder.Services.AddHealthChecks();
var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
//app.Environment.IsDevelopment()

// keep swagger in prod (for now)
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