using System.Text.Json.Serialization;

namespace Services.Steam.Model;

public class PlayerSummaries
{
    [JsonPropertyName("players")]
    public List<PlayerSummary> Players { get; set; } = new List<PlayerSummary>();
}