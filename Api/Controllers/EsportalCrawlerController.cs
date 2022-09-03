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

    [HttpGet("Start")]
    public ServiceResponse<string> Start()
    {
        var res = new ServiceResponse<string>();
        var state = _crawler.Start();
        res.Data = state.ToString();
        Log(state.ToString());
        return res;
    }

    [HttpGet("Stop")]
    public ServiceResponse<string> Stop()
    {
        var res = new ServiceResponse<string>();
        var state = _crawler.Stop();
        res.Data = state.ToString();
        Log(state.ToString());
        return res;
    }

    [HttpGet("Bootstrap")]
    public async Task<ServiceResponse<string>> Bootstrap()
    {
        var res = new ServiceResponse<string>();
        var state = await _crawler.Bootstrap();
        res.Data = state.ToString();
        Log(state.ToString());
        return res;
    }
}
