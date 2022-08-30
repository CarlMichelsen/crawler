

using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class MatchDropDto
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public int Category { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    [JsonPropertyName("user")]
    public UserDto? User { get; set; }

    [JsonPropertyName("match_id")]
    public ulong MatchId { get; set; }

    [JsonPropertyName("inserted")]
    public int Inserted { get; set; }
}