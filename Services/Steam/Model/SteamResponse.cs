using System.Text.Json.Serialization;

namespace Services.Steam.Model;

public class SteamResponse
{
    [JsonPropertyName("response")]
    public PlayerSummaries Response { get; set; } = new PlayerSummaries();
}