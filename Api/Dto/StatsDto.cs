namespace Api.Dto;

public class StatsDto
{
    public int Elo { get; set; }
    public int Level { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Assists { get; set; }
    public int Headshots { get; set; }
    public int Matches { get; set; }
    public int Drops { get; set; }
    public int Mvps { get; set; }
    public int DamageDealt { get; set; }
    public int Rounds { get; set; }
    public int OpeningDuelWins { get; set; }
    public int OpeningKills { get; set; }
    public int OpeningDeaths { get; set; }
    public int Clutches { get; set; }
    public int LongestWinningStreak { get; set; }
    public int LongestLosingStreak { get; set; }
    public int ThumbsUp { get; set; }
    public int ThumbsDown { get; set; }
    public ulong? Rank { get; set; } // world rank
}