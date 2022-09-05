using Microsoft.AspNetCore.Mvc;
using Api.Models;
using WebCrawler;
using Database;
using Database.Repositories;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EsportalCrawlerController : ControllerBase
{
    private readonly ICrawler _crawler;
    private readonly DataContext _context;

    public EsportalCrawlerController(ICrawler crawler, DataContext context)
    {
        _crawler = crawler;
        _context = context;
    }

    private void Log(string input) => Console.WriteLine($"[Crawler] {input}");

    private bool ValidPassword(string password) => string.Equals(password, "teddybear");

    [HttpPost("Start")]
    public ServiceResponse<string> Start([FromBody] string password)
    {
        if (!ValidPassword(password))
        {
            Log("<Start> Invalid password.");
            return new ServiceResponse<string>(){Success = false, Error="Invalid password."};
        }
        var res = new ServiceResponse<string>();
        var state = _crawler.Start();
        if (state == ICrawler.CrawlerResponse.Failure) res.Success = false;

        res.Data = state.ToString();
        Log(state.ToString());
        return res;
    }

    [HttpPost("Stop")]
    public ServiceResponse<string> Stop([FromBody] string password)
    {
        if (!ValidPassword(password))
        {
            Log("<Stop> Invalid password.");
            return new ServiceResponse<string>(){Success = false, Error="Invalid password."};
        }
        var res = new ServiceResponse<string>();
        var state = _crawler.Stop();
        if (state == ICrawler.CrawlerResponse.Failure) res.Success = false;

        res.Data = state.ToString();
        Log(state.ToString());
        return res;
    }

    [HttpPost("Bootstrap")]
    public async Task<ServiceResponse<string>> Bootstrap([FromBody] string password)
    {
        if (!ValidPassword(password))
        {
            Log("<Bootstrap> Invalid password.");
            return new ServiceResponse<string>(){Success = false, Error="Invalid password."};
        }

        var res = new ServiceResponse<string>();
        var state = await _crawler.Bootstrap();
        res.Data = state.ToString();
        if (state == ICrawler.CrawlerResponse.Failure) res.Success = false;
        
        Log(state.ToString());
        return res;
    }

    [HttpGet("Status")]
    public async Task<ServiceResponse<CrawlerStatusResponse>> GetStatus()
    {
        var res = new ServiceResponse<CrawlerStatusResponse>();
        var statusResponse = new CrawlerStatusResponse();

        statusResponse.CrawlerName = "EsportalCrawler";

        statusResponse.ProfileAmount = await EsportalCrawlerStatusRepository.ProfileCount(_context);
        statusResponse.RemainingUnknowns = await EsportalCrawlerStatusRepository.UnknownCount(_context);
        statusResponse.FailedUnknowns = await EsportalCrawlerStatusRepository.FailedUnknownCount(_context);

        var span = DateTime.Now-_crawler.LastStartTime;

        statusResponse.SecondsRunning = span?.Seconds ?? 0;
        statusResponse.Status = _crawler.Status.ToString();
        
        res.Data = statusResponse;
        Log($"Status: {statusResponse.Status.ToString()}");
        return res;
    }
}
