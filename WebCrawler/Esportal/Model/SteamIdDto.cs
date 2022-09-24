using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class SteamIdDto
{
    public bool Success { get; set; }

    public bool Cached { get; set; }

    public string? SteamId { get; set; }
}