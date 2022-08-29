using System.Net.Http.Headers;
using WebCrawler.Esportal.Model;
using System.Text.Json;

namespace WebCrawler.Esportal;

public class EsportalCrawler : ICrawler
{
    public EsportalRequestQueue Queue;
    private ProfileRequestConfig _config;
    private Timer? _timer = null;

    public EsportalCrawler(ProfileRequestConfig config)
    {
        Queue = new EsportalRequestQueue();
        _config = config;
    }

    // does the handling
    private async Task<bool> Handler(string content, HttpResponseHeaders headers) {
        try
        {
            var profile = JsonSerializer.Deserialize<ProfileDto>(content);
            if (profile is null) return false;
            Console.WriteLine("Deserialized!");
            await Task.Delay(50);
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
        if (_timer is not null) return ICrawler.CrawlerResponse.CurrentlyStarted;

        // creaet delegate that runs on handler complete
        IRequestQueue.HandleQueueItem handleItem = async (string content, HttpResponseHeaders headers) => {
            if (content is null) return false;
            return await Handler(content, headers);
        };

        // create delegate that runs on timer (catches throw)
        TimerCallback callback = async(object? obj) => {
            if (obj is null || obj is not EsportalCrawler) throw new InvalidDataException("Time object should be instance of object that implements ICrawler");
            EsportalCrawler val = (EsportalCrawler)obj;
            try
            {
                var success = await val.Queue.HandleNext(handleItem);
            }
            catch (System.Exception e)
            {
                val.Queue.FinalizeNext(false);
                Console.WriteLine(e.Message); // handle list item
            }
        };

        _timer = new(callback, this, TimeSpan.Zero, TimeSpan.FromMilliseconds(2200));
        return ICrawler.CrawlerResponse.Started;
    }

    public ICrawler.CrawlerResponse Stop()
    {
        if (_timer is null) return ICrawler.CrawlerResponse.CurrentlyStopped;
        _timer.Dispose();
        _timer = null;
        return ICrawler.CrawlerResponse.Stopped;
    }
}