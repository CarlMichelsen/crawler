using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class ProfileEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public ulong Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public bool Banned { get; set; }
    public string AvatarHash { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public ulong Flags { get; set; }
    public ProfileConnectionEntity? ProfileConnections { get; set; }
    public StatsEntity Stats { get; set; } = new StatsEntity();
    public RecentStatsEntity RecentStats { get; set; } = new RecentStatsEntity();
    public List<UserEntity> Friends { get; set; } = new List<UserEntity>();
    public List<UsernameEntity> OldUsernames { get; set; } = new List<UsernameEntity>();
    public DateTime Recorded { get; set; }

    public override string ToString()
    {
        System.Text.StringBuilder sb = new(Username);
        sb.Append(new string(' ', Math.Clamp(20-Username.Length, 2, 20)));
        sb.Append(Stats.Elo);
        sb.Append(new string(' ', Math.Clamp(8-Stats.Elo.ToString().Length, 2, 8)));
        sb.Append($">{Id}<");
        sb.Append(new string(' ', Math.Clamp(16-Id.ToString().Length, 2, 16)));
        sb.Append(Recorded.ToString());
        return sb.ToString();
    }
}