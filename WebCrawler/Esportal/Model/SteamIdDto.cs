using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class SteamIdDto
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("cached")]
    public bool Cached { get; set; }

    [JsonPropertyName("transientError")]
    public bool TransientError { get; set; }

    [JsonPropertyName("steamId")]
    public string? SteamId { get; set; }
}