namespace Api.Dto.Query;

public class SteamAccount
{
    public string Username { get; set; } = string.Empty;
    public string RealName { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string AvatarMedium { get; set; } = string.Empty;
    public string AvatarFull { get; set; } = string.Empty;
    public string AvatarHash { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public long TimeCreated { get; set; }
}