using System.Text.Json.Serialization;

namespace Services.Faceit.Model;

public class FaceitSettings
{
    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;
}