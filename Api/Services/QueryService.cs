using Database;
using Api.Dto.Query;
using Services.Faceit;
using Services.Steam;
using Database.Repositories;
using Services.Steam.Model;
using Services.Faceit.Model;
using Api.Dto;
using Database.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Services;

public class QueryService : IQueryService
{
    private readonly DataContext _context;
    private readonly ISteamService _steamService;
    private readonly IFaceitService _faceitService;
    private readonly IMemoryCache _memoryCache;

    public QueryService(DataContext context, ISteamService steamService, IFaceitService faceitService, IMemoryCache memoryCache)
    {
        _context = context;
        _steamService = steamService;
        _faceitService = faceitService;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<QueryResponse>> EsportalUsernameSearch(string query)
    {
        return await _memoryCache.GetOrCreateAsync(
                query,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                    return NonCachedEsportalUsernameSearch(query);
                }
            );
    }

    private async Task<IEnumerable<QueryResponse>> NonCachedEsportalUsernameSearch(string query)
    {
        var results = new List<QueryResponse>();

        var candidates = await SearchRepository.NameSearch(_context, query);
        var tasks = new List<Task<(ProfileEntity, SteamResponse?, FaceitPlayerResponse?)>>();

        foreach (var esportal in candidates) tasks.Add(GetServiceData(esportal));
        var serviceResponseList = await Task.WhenAll(tasks);

        foreach (var response in serviceResponseList)
        {
            var esportal = response.Item1;
            var steamResponse = response.Item2;
            var faceitResponse = response.Item3;

            var q = new QueryResponse
            {
                Esportal = ApiMapper.Mapper.Map<ProfileDto>(esportal)
            };
            if (steamResponse is not null) q.Steam = ApiMapper.MapSteamResponse(steamResponse);
            if (faceitResponse is not null) q.Faceit = ApiMapper.MapFaceitResponse(faceitResponse);

            results.Add(q);
        }

        return results;
    }

    private async Task<(ProfileEntity, SteamResponse?, FaceitPlayerResponse?)> GetServiceData(ProfileEntity esportalProfile)
    {
        var steamId64String = esportalProfile.ProfileConnections?.SteamId64?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(steamId64String)) return (esportalProfile, null, null);
        long steamId64 = long.Parse(steamId64String);

        var steamTask = AttemptGetSteamResponse(steamId64);
        var faceitTask = AttemptGetFaceitResponse(steamId64);
        var steamResponse = await steamTask;
        var faceitResponse = await faceitTask;
        return (esportalProfile, steamResponse, faceitResponse);
    }

    private async Task<SteamResponse?> AttemptGetSteamResponse(long steamId64)
    {
        try
        {
            return await _steamService.UserCounterStrikeStats(steamId64);
        }
        catch (System.Exception)
        {
            return default;
        }
    }
    private async Task<FaceitPlayerResponse?> AttemptGetFaceitResponse(long steamId64)
    {
        try
        {
            return await _faceitService.FaceitPlayer(steamId64);
        }
        catch (System.Exception)
        {
            return default;
        }
    }
}

public struct SearchPayload
{
    public IEnumerable<string> Usernames { get; set; }
    public IEnumerable<long> SteamId64s { get; set; }
}