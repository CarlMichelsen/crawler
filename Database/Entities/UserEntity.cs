using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class UserEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public ulong Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string AvatarHash { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public int DisplayMedals { get; set; }
    public int? Level { get; set; }
    public ulong Flags { get; set; }
    public int RegionId { get; set; }
    public int SubregionId { get; set; }
    public List<ProfileEntity> IncompleteFriends { get; set; } = new List<ProfileEntity>();
}