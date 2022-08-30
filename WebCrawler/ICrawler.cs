namespace WebCrawler;

public interface ICrawler
{
    public enum CrawlerResponse
    {
        Started,
        Stopped,
        CurrentlyStarted,
        CurrentlyStopped,
        Success,
        Failure
    }

    public CrawlerResponse Start();
    public CrawlerResponse Stop();
    public Task<CrawlerResponse> Bootstrap();
}