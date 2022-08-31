using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class UnknownEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public UserEntity User { get; set; } = new UserEntity();
    public DateTime Recorded { get; set; }
}