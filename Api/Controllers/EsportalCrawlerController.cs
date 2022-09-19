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

    private void Log(string input) => Console.WriteLine($"[Crawler] {input}");

    [HttpGet("Status")]
    public async Task<ServiceResponse<string>> GetStatus()
    {
        var res = new ServiceResponse<string>();
        var statusResponse = new CrawlerStatusDto();

        statusResponse.CrawlerName = "EsportalCrawler";

        statusResponse.ProfileAmount = await EsportalCrawlerStatusRepository.ProfileCount(_context);
        statusResponse.RemainingUnknowns = await EsportalCrawlerStatusRepository.UnknownCount(_context);
        statusResponse.FailedUnknowns = await EsportalCrawlerStatusRepository.FailedUnknownCount(_context);
        
        res.Data = statusResponse.ToString();
        Log($"Status: {res.Data}");
        return res;
    }
}
