using Services.Steam.Model;

namespace Services.Steam;

public interface ISteamService
{
    public Task<SteamResponse<PlayerSummaries>> UserCounterStrikeStats(ulong steamId64);
}