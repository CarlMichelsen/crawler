using Database;
using WebCrawler.Esportal;

namespace Api.Configuration;

public class AppConfiguration : IDatabaseConfiguration, ISteamIdUrlConfiguration
{
    public string DatabaseConnectionString { get; }
    public string EsportalSteamIdUrl { get; }

    public AppConfiguration()
    {
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        DatabaseConnectionString = databaseUrl ?? "Server=localhost,1433\\SQLEXPRESS;Database=esportal;MultipleActiveResultSets=True;User Id=SA;Password=ThisIsATestDevelopmentPassword!123;";

        var steamidUrl = Environment.GetEnvironmentVariable("STEAMID_SERVICE_URL");
        EsportalSteamIdUrl = steamidUrl ?? "http://157.245.20.228:8080";
    }
}