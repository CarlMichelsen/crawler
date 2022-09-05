using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class ProfileConnectionEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public string? SteamId64 { get; set; }
}