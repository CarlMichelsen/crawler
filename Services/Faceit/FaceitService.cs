using System.Text.Json;
using Services.Faceit.Model;

namespace Services.Faceit;

public class FaceitService : BaseService, IFaceitService
{
    private readonly IFaceitConfiguration _config;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public FaceitService(IFaceitConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;

        _baseUrl = "https://open.faceit.com";
    }

    public async Task<FaceitPlayerResponse> FaceitPlayer(long steamId64, string game = "csgo")
    {
        Dictionary<string, string> queryStringItems = new()
        {
            { "game", game },
            { "game_player_id", steamId64.ToString() }
        };

        var uri = new Uri($"{_baseUrl}/data/v4/players?{ToQueryString(queryStringItems)}");
        var req = new HttpRequestMessage(HttpMethod.Get, uri);
        req.Headers.Add("Authorization", $"Bearer {_config.FaceitApiKey}");

        var res = await _httpClient.SendAsync(req);
        res.EnsureSuccessStatusCode();

        var responseString = await res.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(responseString))
            throw new Exception("responseString is empty");

        var responseObject = JsonSerializer.Deserialize<FaceitPlayerResponse>(responseString);
        if (responseObject is null)
            throw new NullReferenceException("responseObject is null");

        return responseObject;
    }
}