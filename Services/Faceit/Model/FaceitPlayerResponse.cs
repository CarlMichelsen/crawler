using System.Text.Json.Serialization;

namespace Services.Faceit.Model;

public class FaceitPlayerResponse
{
    [JsonPropertyName("player_id")]
    public Guid PlayerId { get; set; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = string.Empty;

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("cover_image")]
    public string CoverImage { get; set; } = string.Empty;

    // TODO: figure out what is supposed to be in this field
    //[JsonPropertyName("platforms")]
    //public string? Platforms { get; set; } = string.Empty;

    [JsonPropertyName("games")]
    public Dictionary<string, FaceitGame> Games { get; set; } = new Dictionary<string, FaceitGame>();

    [JsonPropertyName("settings")]
    public FaceitSettings Settings { get; set; } = new FaceitSettings();

    [JsonPropertyName("friends_ids")]
    public List<string> FriendsIds { get; set; } = new List<string>();

    [JsonPropertyName("memberships")]
    public List<string> Memberships { get; set; } = new List<string>();

    [JsonPropertyName("faceit_url")]
    public string FaceitUrl { get; set; } = string.Empty;
}