using System.Text.Json.Serialization;

namespace Services.Steam.Model;

public class PlayerSummary
{
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;
    
    [JsonPropertyName("communityvisibilitystate")]
    public int Communityvisibilitystate { get; set; }
    
    [JsonPropertyName("profilestate")]
    public int ProfileState { get; set; }
    
    [JsonPropertyName("personaname")]
    public string Personaname { get; set; } = string.Empty;

    [JsonPropertyName("commentpermission")]
    public int CommentPermission { get; set; }

    [JsonPropertyName("profileurl")]
    public string Profileurl { get; set; } = string.Empty;

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = string.Empty;

    [JsonPropertyName("avatarmedium")]
    public string AvatarMedium { get; set; } = string.Empty;

    [JsonPropertyName("avatarfull")]
    public string AvatarFull { get; set; } = string.Empty;

    [JsonPropertyName("avatarhash")]
    public string AvatarHash { get; set; } = string.Empty;

    [JsonPropertyName("lastlogoff")]
    public long LastLogoff { get; set; }
    
    [JsonPropertyName("personastate")]
    public int PersonaState { get; set; }

    [JsonPropertyName("realname")]
    public string RealName { get; set; } = string.Empty;
    
    [JsonPropertyName("primaryclanid")]
    public string PrimaryClanId { get; set; } = string.Empty;
    
    [JsonPropertyName("timecreated")]
    public long TimeCreated { get; set; }
    
    [JsonPropertyName("personastateflags")]
    public long PersonaStateFlags { get; set; }
    
    [JsonPropertyName("loccountrycode")]
    public string LocCountryCode { get; set; } = string.Empty;

    [JsonPropertyName("locstatecode")]
    public string LocStateCode { get; set; } = string.Empty;
}