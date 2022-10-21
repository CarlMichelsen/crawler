using System.Text.Json;
using Services.Steam.Model;

namespace Services.Steam;

public class SteamService : BaseService, ISteamService
{
    private readonly ISteamServiceConfiguration _config;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public SteamService(ISteamServiceConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;

        _baseUrl = "https://api.steampowered.com";
    }

    public async Task<SteamResponse> UserCounterStrikeStats(long steamId64)
    {
        Dictionary<string, string> queryStringItems = new()
        {
            { "key", _config.SteamWebApiKey },
            { "steamids", steamId64.ToString() }
        };

        var uri = new Uri($"{_baseUrl}/ISteamUser/GetPlayerSummaries/v2?{ToQueryString(queryStringItems)}");
        var req = new HttpRequestMessage(HttpMethod.Get, uri);
        var res = await _httpClient.SendAsync(req);
        res.EnsureSuccessStatusCode();

        var responseString = await res.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(responseString))
            throw new Exception("responseString is empty");

        var responseObject = JsonSerializer.Deserialize<SteamResponse>(responseString);
        if (responseObject is null)
            throw new NullReferenceException("responseObject is null");

        return responseObject;
    }
}