using System.Net.Http.Headers;

namespace WebCrawler;

public interface IRequestQueue
{
    public delegate Task<bool> HandleQueueItem(string content, HttpResponseHeaders headers);
    public Task<bool> HandleNext(HandleQueueItem handler);
    public Task<Uri?> GetNext();
    public void FinalizeNext(bool success);
}