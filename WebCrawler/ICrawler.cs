namespace WebCrawler;

public interface ICrawler
{
    public enum CrawlerResponse
    {
        Started,
        Stopped,
        CurrentlyStarted,
        CurrentlyStopped,
        Failure
    }

    public CrawlerResponse Start();
    public CrawlerResponse Stop();
}