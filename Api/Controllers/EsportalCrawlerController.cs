using Microsoft.AspNetCore.Mvc;
using Database;
using Database.Repositories;
using Api.Dto;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EsportalCrawlerController : ControllerBase
{
    private readonly ILogger<EsportalCrawlerController> _logger;
    private readonly DataContext _context;

    public EsportalCrawlerController(ILogger<EsportalCrawlerController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("Status")]
    public async Task<ServiceResponse<CrawlerStatusDto>> GetStatus()
    {
        var res = new ServiceResponse<CrawlerStatusDto>();
        var statusResponse = new CrawlerStatusDto
        {
            CrawlerName = "EsportalCrawler",
            ProfileAmount = await EsportalCrawlerStatusRepository.ProfileCount(_context),
            RemainingUnknowns = await EsportalCrawlerStatusRepository.UnknownCount(_context),
            FailedUnknowns = await EsportalCrawlerStatusRepository.FailedUnknownCount(_context),
            SteamIdCount = await EsportalCrawlerStatusRepository.SteamIdCount(_context)
        };

        res.Data = statusResponse;
        _logger.LogInformation("Status: {status}", res.Data);
        return res;
    }

    [HttpPost("PurgeFailedSteamIdFetchList")]
    public async Task<ServiceResponse<int>> PurgeFailedSteamIdFetchList()
    {
        var res = new ServiceResponse<int>();
        try
        {
            res.Data = await EsportalSteamIdRepository.PurgeAllFailedSteamId(_context);
            _logger.LogInformation("Purged {} failed steamid fetches", res.Data);
        }
        catch (Exception e)
        {
            _logger.LogError("PurgeAllFailedSteamId operation failed: {}\n{}", e.Message, e.InnerException);
            res.Success = false;
            res.Error = "PurgeAllFailedSteamId operation failed.";
        }
        return res;
    }
}
