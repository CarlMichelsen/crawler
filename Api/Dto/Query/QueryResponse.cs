namespace Api.Dto.Query;

public class QueryResponse
{
    public ProfileDto Esportal { get; set; } = new ProfileDto();
    public SteamAccount? Steam { get; set; }
    public FaceitAccount? Faceit { get; set; }
}