using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class UserDto {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("avatar_hash")]
    public string AvatarHash { get; set; } = string.Empty;

    [JsonPropertyName("country_id")]
    public int CountryId { get; set; }

    [JsonPropertyName("display_medals")]
    public int DisplayMedals { get; set; }

    [JsonPropertyName("flags")]
    public long Flags { get; set; }

    [JsonPropertyName("region_id")]
    public int RegionId { get; set; }

    [JsonPropertyName("subregion_id")]
    public int SubregionId { get; set; }

    //[JsonPropertyName("permissions")]
    //public null Permissions { get; set; }
}