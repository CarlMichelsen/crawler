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

    [HttpGet("Start/{password}")]
    public ServiceResponse<string> Start([FromRoute] string password)
    {
        if (!ValidPassword(password))
        {
            Log("<Start> Invalid password.");
            return new ServiceResponse<string>(){Success = false, Error="Invalid password."};
        }
        var res = new ServiceResponse<string>();
        var state = _crawler.Start();
        res.Data = state.ToString();
        Log(state.ToString());
        return res;
    }

    [HttpGet("Stop/{password}")]
    public ServiceResponse<string> Stop([FromRoute] string password)
    {
        if (!ValidPassword(password))
        {
            Log("<Stop> Invalid password.");
            return new ServiceResponse<string>(){Success = false, Error="Invalid password."};
        }
        var res = new ServiceResponse<string>();
        var state = _crawler.Stop();
        res.Data = state.ToString();
        Log(state.ToString());
        return res;
    }

    [HttpGet("Bootstrap/{password}")]
    public async Task<ServiceResponse<string>> Bootstrap([FromRoute] string password)
    {
        if (!ValidPassword(password))
        {
            Log("<Bootstrap> Invalid password.");
            return new ServiceResponse<string>(){Success = false, Error="Invalid password."};
        }
        var res = new ServiceResponse<string>();
        var state = await _crawler.Bootstrap();
        res.Data = state.ToString();
        Log(state.ToString());
        return res;
    }
}
