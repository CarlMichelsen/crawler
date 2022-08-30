using Database.Entities; //only needed for bootstrap

namespace WebCrawler.Esportal;

public class EsportalCrawler : ICrawler
{
    public EsportalRequestHandler Handler;
    private Timer? _timer = null;

    public EsportalCrawler()
    {
        Handler = new EsportalRequestHandler();
    }

    public ICrawler.CrawlerResponse Start()
    {
        if (_timer is not null) return ICrawler.CrawlerResponse.CurrentlyStarted;
        _timer = new(TimerCallbackFactory(), Handler, TimeSpan.Zero, TimeSpan.FromMilliseconds(2200));
        return ICrawler.CrawlerResponse.Started;
    }

    public ICrawler.CrawlerResponse Stop()
    {
        try
        {
            if (_timer is null) return ICrawler.CrawlerResponse.CurrentlyStopped;
            _timer.Dispose();
            _timer = null;
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            return ICrawler.CrawlerResponse.Failure;
        }
        
        return ICrawler.CrawlerResponse.Stopped;
    }

    public async Task<ICrawler.CrawlerResponse> Bootstrap()
    {
        try
        {
            var next = BootstrapUserEntity();
            var result = await Handler.HandleNext(next);
            var profileWasAdded = await Handler.FinalizeNext(next, result);
            return profileWasAdded ? ICrawler.CrawlerResponse.Success : ICrawler.CrawlerResponse.Failure;
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"Bootstrap failed\n{e.Message}");
            return ICrawler.CrawlerResponse.Failure;
        }
    }

    private UserEntity BootstrapUserEntity()
    {
        // mock bootstrap data taken 30. aug. 2022
        var ent = new UserEntity();
        ent.Id = 957283692;
        ent.Username = "mag";
        ent.AvatarHash = "ecfef2bf5b1146c96566d92b6d8b663cb0f4d2c3";
        ent.CountryId = 57;
        ent.DisplayMedals = 115587185;
        ent.Level = null;
        ent.Flags = 271679521;
        ent.RegionId = 0;
        ent.SubregionId = 0;
        return ent;
    }

    private TimerCallback TimerCallbackFactory()
    {
        TimerCallback callback = async (object? obj) => {
            if (obj is not EsportalRequestHandler) return;
            var handler = (EsportalRequestHandler)obj;

            try
            {
                var next = await handler.GetNext();
                var result = await handler.HandleNext(next);
                await handler.FinalizeNext(next, result);
            }
            catch (System.Exception)
            {
                await handler.FinalizeNext(null, null);
            }
        };
        return callback;
    }
}