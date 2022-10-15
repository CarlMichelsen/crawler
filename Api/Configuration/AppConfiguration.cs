using Database;
using WebCrawler.Esportal;
using Services.Steam;
using Services.Faceit;

namespace Api.Configuration;

public class AppConfiguration:
    IAuthConfiguration,
    IDatabaseConfiguration,
    ISteamIdConfiguration,
    ISteamServiceConfiguration,
    IFaceitConfiguration
{
    private readonly IDevConfigurationReader _dev;

    public string DatabaseConnectionString { get; }
    public string EsportalSteamIdUrl { get; }
    public string SteamWebApiKey { get; }
    public string CounterStrikeAppId { get; }
    public string FaceitApiKey { get; }

    public AppConfiguration(IDevConfigurationReader dev)
    {
        _dev = dev;

        DatabaseConnectionString = AttemptLoad("DATABASE_URL", true);
        EsportalSteamIdUrl = AttemptLoad("STEAMID_SERVICE_URL", true);
        SteamWebApiKey = AttemptLoad("STEAMAPI_KEY", true);
        CounterStrikeAppId = AttemptLoad("COUNTERSTRIKE_APPID", true);
        FaceitApiKey = AttemptLoad("FACEITAPI_KEY", true);

        Console.WriteLine($"CONFIG\nDatabaseConnectionString='{DatabaseConnectionString}'\nEsportalSteamIdUrl='{EsportalSteamIdUrl}'\nSteamWebApiKey='{SteamWebApiKey}'\nCounterStrikeAppId='{CounterStrikeAppId}'\nFaceitApiKey='{FaceitApiKey}'");
    }

    private string AttemptLoad(string key, bool required = false)
    {
        var devExsists = _dev.Configuration.TryGetValue(key, out string? value);
        var envValue = Environment.GetEnvironmentVariable(key);
        if (envValue is not null && !devExsists) value = envValue;
        if (value is not null)
        {
            return value;
        }
        else if (required)
        {
            throw new Exception($"Missing required configuration variable \"{key}\"");
        }
        return string.Empty;
    }
}