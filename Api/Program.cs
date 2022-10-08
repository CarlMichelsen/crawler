using Database;
using BackgroundServices;
using WebCrawler.Esportal;
using WebCrawler.Esportal.Services;
using Api.Configuration;
using Services.Steam;

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
    .AddTransient<EsportalProfileService>()
    .AddTransient<ISteamService, SteamService>();

// config
builder.Services // TODO: single source of truth
    .AddSingleton<IDatabaseConfiguration, AppConfiguration>()
    .AddSingleton<ISteamIdUrlConfiguration, AppConfiguration>()
    .AddSingleton<ISteamServiceConfiguration, AppConfiguration>()
    .AddSingleton<DevConfiguration>();

builder.Services.AddDbContext<DataContext>();

builder.Services.AddHostedService<EsportalBackgroundService>();
builder.Services.AddHostedService<EsportalSteamIdBackgroundService>();

builder.Services.AddHttpClient<EsportalBackgroundService>();
builder.Services.AddHttpClient<SteamService>();

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