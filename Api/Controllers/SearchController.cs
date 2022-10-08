using Microsoft.AspNetCore.Mvc;
using Database.Repositories;
using Database;
using Api.Dto;
using Services.Steam;
using Database.Search;
using Services.Steam.Model;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly DataContext _context;
    private readonly ISteamService _steamService;

    public SearchController(ILogger<SearchController> logger ,DataContext context, ISteamService steamService)
    {
        _logger = logger;
        _context = context;
        _steamService = steamService;
    }

    private static void Log(string input)
    {
        Console.WriteLine($"[Search] {input}");
    }

    [HttpGet("Username/{q}")]
    public async Task<ServiceResponse<List<ProfileDto>>> Username([FromRoute] string q)
    {
        var res = new ServiceResponse<List<ProfileDto>>();
        try
        {
            _logger.LogInformation("Starting username search: {search}", q);
            var resultList = await SearchRepository.NameSearch(_context, q);
            List<ProfileDto> dtoList = resultList.Select(p => ApiMapper.Mapper.Map<ProfileDto>(p)).ToList();
            _logger.LogInformation("Found {count} results from username search: \"{username}\"", resultList.Count, q);
            res.Data = dtoList;
        }
        catch (Exception e)
        {
            Log(e.Message);
            res.Success = false;
            res.Error = "Search failed.";
        }
        return res;
    }

    [HttpPost("Filter")]
    public async Task<ServiceResponse<List<ProfileDto>>> Filter([FromBody] SearchFilter search)
    {
        var res = new ServiceResponse<List<ProfileDto>>();
        try
        {
            _logger.LogInformation("Starting filter search: {search}", search);
            var resultList = await SearchRepository.FilterSearch(_context, search);
            List<ProfileDto> dtoList = resultList.Select(p => ApiMapper.Mapper.Map<ProfileDto>(p)).ToList();
            _logger.LogInformation("Found {count} results from filter search: \"{search}\"", resultList.Count, search);
            res.Data = dtoList;
        }
        catch (Exception e)
        {
            Log(e.Message);
            res.Success = false;
            res.Error = "Search failed.";
        }
        return res;
    }

    [HttpPost("Count")]
    public async Task<ServiceResponse<int>> Count([FromBody] SearchRange range)
    {
        var res = new ServiceResponse<int>();
        try
        {
            _logger.LogInformation("Starting count search: {search}", range);
            var resultCount = await SearchRepository.EloCountSearch(_context, range);
            _logger.LogInformation("Found {count} results from count search: \"{search}\"", resultCount, range);
            res.Data = resultCount;
        }
        catch (Exception e)
        {
            Log(e.Message);
            res.Success = false;
            res.Error = "Search failed.";
        }
        return res;
    }

    [HttpGet("SteamUser/{steamId64}")]
    public async Task<ServiceResponse<List<PlayerSummary>>> Steam([FromRoute] ulong steamId64)
    {
        var res = new ServiceResponse<List<PlayerSummary>>();
        try
        {
            var steamResponse = await _steamService.UserCounterStrikeStats(steamId64);
            res.Data = steamResponse?.Response?.Players ?? new List<PlayerSummary>();
        }
        catch (Exception e)
        {
            Log(e.Message);
            res.Success = false;
            res.Error = "Search failed.";
        }
        return res;
    }
}
