using Microsoft.AspNetCore.Mvc;
using Database;
using Database.Repositories;
using Api.Dto;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EsportalCrawlerController : ControllerBase
{
    private readonly DataContext _context;

    public EsportalCrawlerController(DataContext context)
    {
        _context = context;
    }

    private static void Log(string input) => Console.WriteLine($"[Crawler] {input}");

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
        Log($"Status: {res.Data}");
        return res;
    }
}
