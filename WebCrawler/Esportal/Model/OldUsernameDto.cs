using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class OldUsername
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("inserted")]
    public ulong Inserted { get; set; }
}