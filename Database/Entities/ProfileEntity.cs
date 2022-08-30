namespace Database.Entities;

public class ProfileEntity
{
    public ulong Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public bool Banned { get; set; }
    public string AvatarHash { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public ulong Flags { get; set; }
    public StatsEntity Stats { get; set; } = new StatsEntity();
    public RecentStatsEntity RecentStats { get; set; } = new RecentStatsEntity();
    public List<MatchEntity> MatchDrops { get; set; } = new List<MatchEntity>();
    public List<UserEntity> Friends { get; set; } = new List<UserEntity>();
    public List<UsernameEntity> OldUsernames { get; set; } = new List<UsernameEntity>();
}