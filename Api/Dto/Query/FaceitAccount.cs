namespace Api.Dto.Query;

public class FaceitAccount
{
    public string FaceitId { get; set; } = string.Empty;
    public string FaceitUsername { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string CoverImage { get; set; } = string.Empty;
    public int SkillLevel { get; set; }
    public int Elo { get; set; }
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string FaceitUrl { get; set; } = string.Empty;
    public int Friends { get; set; }
}