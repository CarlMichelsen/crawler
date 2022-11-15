
using Microsoft.Extensions.Caching.Memory;
using Services.Faceit;
using Services.Faceit.Model;

namespace Api.Services.Cached;

public class CachedFaceitService : IFaceitService
{
    private readonly IMemoryCache _memoryCache;
    private readonly FaceitService _service;

    public CachedFaceitService(FaceitService service, IMemoryCache memoryCache)
    {
        _service = service;
        _memoryCache = memoryCache;
    }

    public async Task<FaceitPlayerResponse> FaceitPlayer(long steamId64, string game = "csgo")
    {
        var key = $"{steamId64}:{game.Trim().ToLower()}";
        return await _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromDays(3));
                    return _service.FaceitPlayer(steamId64, game);
                }
            );
    }
}