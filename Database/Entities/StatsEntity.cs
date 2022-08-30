namespace Database.Entities;

public class StatsEntity
{
    public ulong Id { get; set; }
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
    public int TopFrags { get; set; }
    public int OvertimeWins { get; set; }
    public int Dominations { get; set; }
    public int Scores100 { get; set; }
    public int kdOver3 { get; set; }
    public int BombPlants { get; set; }
    public int BombDefuses { get; set; }
    public int Aces { get; set; }
    public int OneHpSurvivals { get; set; }
    public int EcoAces { get; set; }
    public int EcoWins { get; set; }
    public int FragSteals { get; set; }
    public int AwpDuelWins { get; set; }
    public int BananaKills { get; set; }
    public int MidDustKills { get; set; }
    public int GamesAsLegend { get; set; }
    public ulong PremiumElapsedTime { get; set; }
    public int ThumbsUp { get; set; }
    public int ThumbsDown { get; set; }
    public ulong? Rank { get; set; } // world rank
    
    // Gather
    public int GathersPlayed { get; set; }
    public int GathersCreated { get; set; }
    public int GatherDrops { get; set; }
}