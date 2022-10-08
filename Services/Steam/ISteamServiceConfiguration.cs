namespace Services.Steam;

public interface ISteamServiceConfiguration
{
    public string SteamWebApiKey { get; }
    public string CounterStrikeAppId { get; }
}