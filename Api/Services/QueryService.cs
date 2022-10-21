using Database;
using Api.Dto.Query;
using Services.Faceit;
using Services.Steam;
using Database.Repositories;
using Services.Steam.Model;
using Services.Faceit.Model;
using Api.Dto;

namespace Api.Services;

public class QueryService : IQueryService
{
    private readonly DataContext _context;
    private readonly ISteamService _steamService;
    private readonly IFaceitService _faceitService;

    public QueryService(DataContext context, ISteamService steamService, IFaceitService faceitService)
    {
        _context = context;
        _steamService = steamService;
        _faceitService = faceitService;
    }

    public async Task<IEnumerable<QueryResponse>> Search(string query)
    {
        var results = new List<QueryResponse>();

        var candidates = await SearchRepository.NameSearch(_context, query);
        foreach (var esportal in candidates)
        {
            SteamResponse? steamResponse = null;
            FaceitPlayerResponse? faceitResponse = null;

            if (esportal.ProfileConnections?.SteamId64 is not null)
            {
                long steamId64 = long.Parse(esportal.ProfileConnections.SteamId64.Trim());
                steamResponse = await AttemptGetSteamResponse(steamId64);
                faceitResponse = await AttemptGetFaceitResponse(steamId64);
            }

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