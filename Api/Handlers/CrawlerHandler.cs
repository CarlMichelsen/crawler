using WebCrawler.Esportal;
using WebCrawler;

namespace Api.Handlers;

public class CrawlerHandler {
    private static CrawlerHandler? _instance;
    public static CrawlerHandler Instance {
        get {
            if (_instance is null) _instance = new CrawlerHandler();
            return _instance;
        }
        private set => _instance = value;
    }

    private EsportalCrawler _crawler;

    private CrawlerHandler()
    {
        _crawler = new EsportalCrawler();
    }

    public string Start()
    {
        return Enum.GetName(typeof(ICrawler.CrawlerResponse), _crawler.Start()) ?? string.Empty;
    }

    public string Stop()
    {
        return Enum.GetName(typeof(ICrawler.CrawlerResponse), _crawler.Stop()) ?? string.Empty;
    }
}