using System.Net.Http.Headers;

namespace WebCrawler;

public interface IWebRequestQueue
{
    public delegate Task<bool> HandleQueueItem(string content, HttpResponseHeaders headers);
    Task<bool> Next(HandleQueueItem handler);
    bool Push(Uri input);
    bool Push(string input);
}