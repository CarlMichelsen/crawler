using Services.Faceit.Model;

namespace Services.Faceit;

public interface IFaceitService
{
    public Task<FaceitPlayerResponse> FaceitPlayer(long steamId64, string game = "csgo");
}