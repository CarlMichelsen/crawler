using WebCrawler.Esportal;
using WebCrawler;

namespace Api;

public class CrawlerSingleton {
    private static CrawlerSingleton? _instance;
    public static CrawlerSingleton Instance {
        get {
            if (_instance is null) _instance = new CrawlerSingleton();
            return _instance;
        }
        private set => _instance = value;
    }

    private ICrawler _crawler;

    private CrawlerSingleton()
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

    public async Task<string> Bootstrap()
    {
        return Enum.GetName(typeof(ICrawler.CrawlerResponse), await _crawler.Bootstrap()) ?? string.Empty;
    }
}