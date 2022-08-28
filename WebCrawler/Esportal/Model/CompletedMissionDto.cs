using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class CompletedMissionDto
{
    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("mission")]
    public int Mission { get; set; }

    [JsonPropertyName("inserted")]
    public long Inserted { get; set; }
}