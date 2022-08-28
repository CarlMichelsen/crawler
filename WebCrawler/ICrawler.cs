namespace WebCrawler;

public interface ICrawler
{
    public enum CrawlerResponse
    {
        Started,
        Stopped,
        AlreadyStarted,
        AlreadyStopped,
        Failure
    }

    public CrawlerResponse Start();
    public CrawlerResponse Stop();
}