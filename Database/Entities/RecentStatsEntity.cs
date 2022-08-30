using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class RecentStatsEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public int RecentKills { get; set; }
    public int RecentDeaths { get; set; }
    public int RecentHeadshots { get; set; }
    public int RecentWins { get; set; }
    public int RecentLosses { get; set; }
    public int RecentMatches { get; set; }
    public int RecentDrops { get; set; }
}