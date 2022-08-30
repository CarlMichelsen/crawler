namespace Database.Entities;

public class MatchEntity
{
    public ulong Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Category { get; set; }
    public string Image { get; set; } = string.Empty;
    public UserEntity? User { get; set; }
    public ulong MatchId { get; set; }
    public DateTime Inserted { get; } = DateTime.Now;
}