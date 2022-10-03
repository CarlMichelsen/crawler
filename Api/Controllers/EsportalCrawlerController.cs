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
    public async Task<ServiceResponse<string>> GetStatus()
    {
        var res = new ServiceResponse<string>();
        var statusResponse = new CrawlerStatusDto
        {
            CrawlerName = "EsportalCrawler",
            ProfileAmount = await EsportalCrawlerStatusRepository.ProfileCount(_context),
            RemainingUnknowns = await EsportalCrawlerStatusRepository.UnknownCount(_context),
            FailedUnknowns = await EsportalCrawlerStatusRepository.FailedUnknownCount(_context),
            SteamIdCount = await EsportalCrawlerStatusRepository.SteamIdCount(_context)
        };

        res.Data = statusResponse.ToString();
        _logger.LogInformation("Status: {status}", res.Data);
        return res;
    }
}
