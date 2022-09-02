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
        System.Console.WriteLine($"[Crawler] {input}");
    }

    [HttpGet("Start")]
    public ServiceResponse<(ICrawler.CrawlerResponse, string)> Start()
    {
        var res = new ServiceResponse<(ICrawler.CrawlerResponse, string)>();
        var state = _crawler.Start();
        res.Data = (state, state.ToString());
        Log(state.ToString());
        return res;
    }

    [HttpGet("Stop")]
    public ServiceResponse<(ICrawler.CrawlerResponse, string)> Stop()
    {
        var res = new ServiceResponse<(ICrawler.CrawlerResponse, string)>();
        var state = _crawler.Stop();
        res.Data = (state, state.ToString());
        Log(state.ToString());
        return res;
    }

    [HttpGet("Bootstrap")]
    public async Task<ServiceResponse<(ICrawler.CrawlerResponse, string)>> Bootstrap()
    {
        var res = new ServiceResponse<(ICrawler.CrawlerResponse, string)>();
        var state = await _crawler.Bootstrap();
        res.Data = (state, state.ToString());
        Log(state.ToString());
        return res;
    }
}
