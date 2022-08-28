using System.Net.Http.Headers;
using WebCrawler.Esportal.Model;
using System.Text.Json;

namespace WebCrawler.Esportal;

public class EsportalCrawler : ICrawler
{
    public WebRequestQueue Queue;
    private ProfileRequestConfig _config;
    private Timer? _timer = null;

    private Uri EsportalUri(ProfileRequestConfig config) {
        var protocol = "https";
        var host = "esportal.com";
        var path = "api/user_profile/get";
        return new Uri($"{protocol}://{host}/{path}?{config.Query()}");
    }

    public EsportalCrawler(ProfileRequestConfig config)
    {
        Console.WriteLine(EsportalUri(config));
        Queue = new WebRequestQueue(EsportalUri(config));
        for (int i = 0; i < 50; i++)
        {
            Queue.Push(EsportalUri(config));
        }
        
        _config = config;
    }

    private async Task<bool> Handler(string content, HttpResponseHeaders headers) {
        try
        {
            var profile = JsonSerializer.Deserialize<ProfileDto>(content);
            if (profile is null) return false;

            Console.WriteLine(profile.Username);
            await Task.Delay(500);
        }
        catch (System.Exception)
        {
            Console.WriteLine(content);
            throw;
        }
        return true;
    }

    public ICrawler.CrawlerResponse Start()
    {
        if (_timer is not null) return ICrawler.CrawlerResponse.AlreadyStarted;

        IWebRequestQueue.HandleQueueItem handleItem = async (string content, HttpResponseHeaders headers) => {
            if (content is null) return false;
            return await Handler(content, headers);
        };

        TimerCallback callback = async(object? obj) => {
            if (obj is null || obj is not EsportalCrawler) throw new InvalidDataException("Time object should be instance of object that implements ICrawler");
            EsportalCrawler val = (EsportalCrawler)obj;
            await val.Queue.Next(handleItem);
        };

        _timer = new(callback, this, TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(2500));
        return ICrawler.CrawlerResponse.Started;
    }

    public ICrawler.CrawlerResponse Stop()
    {
        if (_timer is null) return ICrawler.CrawlerResponse.AlreadyStopped;
        _timer.Dispose();
        _timer = null;
        return ICrawler.CrawlerResponse.Stopped;
    }
}