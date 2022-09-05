using Microsoft.AspNetCore.Mvc;
using Api.Models;
using WebCrawler;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EsportalCrawlerController : ControllerBase
{
    private readonly ICrawler _crawler;

    public EsportalCrawlerController(ICrawler crawler)
    {
        _crawler = crawler;
    }

    private void Log(string input)
    {
        Console.WriteLine($"[Crawler] {input}");
    }

    private bool ValidPassword(string password)
    {
        return string.Equals(password, "teddybear");
    }

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
    public ServiceResponse<string> GetStatus()
    {
        var res = new ServiceResponse<string>();
        res.Data = _crawler.Status.ToString();
        Log($"Status: {_crawler.Status.ToString()}");
        return res;
    }
}
