namespace WebCrawler.Esportal;

public class EsportalSteamCrawler : ICrawler
{
    public ICrawler.CrawlerResponse Status => throw new NotImplementedException();

    public DateTime? LastStartTime => throw new NotImplementedException();

    public Task<ICrawler.CrawlerResponse> Bootstrap()
    {
        throw new NotImplementedException();
    }

    public ICrawler.CrawlerResponse Start()
    {
        throw new NotImplementedException();
    }

    public ICrawler.CrawlerResponse Stop()
    {
        throw new NotImplementedException();
    }
}