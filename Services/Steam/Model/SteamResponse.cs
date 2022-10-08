using System.Text.Json.Serialization;

namespace Services.Steam.Model;

public class SteamResponse<T>
{
    [JsonPropertyName("response")]
    public T? Response { get; set; }
}