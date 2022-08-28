using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class OldUsername
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("inserted")]
    public long Inserted { get; set; }
}