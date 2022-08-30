using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class UsernameEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public string Username { get; set; } = string.Empty;
}