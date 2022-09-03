namespace Api.Dto;

public class ProfileDto
{
    public string Username { get; set; } = string.Empty;
    public bool Banned { get; set; }
    public string AvatarHash { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public StatsDto Stats { get; set; } = new StatsDto();
    public RecentStatsDto RecentStats { get; set; } = new RecentStatsDto();
    public List<string> Friends { get; set; } = new List<string>();
    public List<string> OldUsernames { get; set; } = new List<string>();
    public DateTime Recorded { get; set; }
}