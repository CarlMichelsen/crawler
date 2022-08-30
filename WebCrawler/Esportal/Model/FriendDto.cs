using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class FriendDto
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("avatar_hash")]
    public string AvatarHash { get; set; } = string.Empty;

    [JsonPropertyName("country_id")]
    public int CountryId { get; set; }

    [JsonPropertyName("display_medals")]
    public int DisplayMedals { get; set; }

    [JsonPropertyName("flags")]
    public ulong Flags { get; set; }

    [JsonPropertyName("region_id")]
    public int RegionId { get; set; }

    [JsonPropertyName("subregion_id")]
    public int SubregionId { get; set; }

    [JsonPropertyName("online_status")]
    public int OnlineStatus { get; set; }
}

