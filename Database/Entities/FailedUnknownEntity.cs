using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class FailedUnknownEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public ulong UserId { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime Recorded { get; set; }
}