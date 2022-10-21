using Services.Steam.Model;

namespace Services.Steam;

public interface ISteamService
{
    public Task<SteamResponse> UserCounterStrikeStats(long steamId64);
}