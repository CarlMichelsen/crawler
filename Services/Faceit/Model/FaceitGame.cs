using System.Text.Json.Serialization;

namespace Services.Faceit.Model;


public class FaceitGame
{
    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("game_player_id")]
    public string GamePlayerId { get; set; } = string.Empty; // example: steamId64 in csgo

    [JsonPropertyName("skill_level")]
    public int SkillLevel { get; set; }

    [JsonPropertyName("faceit_elo")]
    public int FaceitElo { get; set; }

    [JsonPropertyName("game_player_name")]
    public string GamePlayerName { get; set; } = string.Empty;

    [JsonPropertyName("skill_level_label")]
    public string SkillLevelLabel { get; set; } = string.Empty;

    // TODO: figure out what is supposed to be in this field
    //[JsonPropertyName("regions")]
    //public string Regions { get; set; } = string.Empty;

    [JsonPropertyName("game_profile_id")]
    public string GameProfileId { get; set; } = string.Empty;
}