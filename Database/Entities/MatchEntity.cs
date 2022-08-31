using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class MatchEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Category { get; set; }
    public string Image { get; set; } = string.Empty;
    public UserEntity? User { get; set; }
    public ulong MatchId { get; set; }
    public DateTime? Recorded { get; set; }
}