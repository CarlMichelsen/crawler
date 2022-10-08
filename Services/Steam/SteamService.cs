using System.Text.Json;
using Services.Steam.Model;

namespace Services.Steam;

public class SteamService : ISteamService
{
    private readonly ISteamServiceConfiguration _config;
    public readonly HttpClient _httpClient;

    public SteamService(ISteamServiceConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<SteamResponse<PlayerSummaries>> UserCounterStrikeStats(ulong steamId64)
    {
        var baseUrl = "https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/";

        Dictionary<string, string> queryStringItems = new()
        {
            { "key", _config.SteamWebApiKey },
            { "steamids", steamId64.ToString() }
        };

        var uri = new Uri($"{baseUrl}?{ToQueryString(queryStringItems)}");
        var req = new HttpRequestMessage(HttpMethod.Get, uri);
        var res = await _httpClient.SendAsync(req);
        res.EnsureSuccessStatusCode();

        var responseString = await res.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(responseString)) 
            throw new Exception("responseString is empty");
        
        var responseObject = JsonSerializer.Deserialize<SteamResponse<PlayerSummaries>>(responseString);
        if (responseObject is null)
            throw new NullReferenceException("responseObject is null");
 
        return responseObject;
    }

    private static string ToQueryString(Dictionary<string, string> keyValues)
    {
        var stringItems = keyValues.Select(kv => $"{System.Web.HttpUtility.UrlEncode(kv.Key)}={System.Web.HttpUtility.UrlEncode(kv.Value)}");
        return $"{string.Join("&", stringItems)}";
    }
}