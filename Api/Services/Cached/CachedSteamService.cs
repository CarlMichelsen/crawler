using Microsoft.Extensions.Caching.Memory;
using Services.Steam;
using Services.Steam.Model;

namespace Api.Services.Cached;

public class CachedSteamService : ISteamService
{
    private readonly IMemoryCache _memoryCache;
    private readonly SteamService _service;

    public CachedSteamService(SteamService service, IMemoryCache memoryCache)
    {
        _service = service;
        _memoryCache = memoryCache;
    }

    public async Task<SteamResponse> UserCounterStrikeStats(long steamId64)
    {
        return await _memoryCache.GetOrCreateAsync(
                steamId64,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                    return _service.UserCounterStrikeStats(steamId64);
                }
            );
    }
}