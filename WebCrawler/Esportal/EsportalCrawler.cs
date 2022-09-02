using Database.Entities;

namespace WebCrawler.Esportal;

public class EsportalCrawler : ICrawler
{
    public EsportalRequestHandler Handler;
    private Timer? _timer = null;

    private UnknownEntity? _currentUnknownEntity = null; // keep this in backpocket in case something catastropic happens

    public EsportalCrawler()
    {
        Handler = new EsportalRequestHandler();
    }

    public ICrawler.CrawlerResponse Start()
    {
        if (_timer is not null) return ICrawler.CrawlerResponse.CurrentlyStarted;
        _timer = new(TimerCallbackFactory(), Handler, TimeSpan.Zero, TimeSpan.FromMilliseconds(2500));
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
            var attempt = await Handler.GetNext();
            if (attempt is not null) throw new Exception("No bootstrap was needed");

            var next = BootstrapUserEntity();
            var result = await Handler.HandleNext(next);
            var profileWasAdded = await Handler.FinalizeNext(next, result, null);
            return profileWasAdded ? ICrawler.CrawlerResponse.Success : ICrawler.CrawlerResponse.Failure;
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"Bootstrap failed\n{e.Message}");
            Console.WriteLine(e.InnerException);
            return ICrawler.CrawlerResponse.Failure;
        }
    }

    private void NoMoreItems()
    {
        var stop = Stop();
        Console.WriteLine($"No more items left {stop}");
    }

    private UnknownEntity BootstrapUserEntity()
    {
        // mock bootstrap data taken 30. aug. 2022
        var usr = new UserEntity();
        usr.Id = 957283692;
        usr.Username = "mag";
        usr.AvatarHash = "ecfef2bf5b1146c96566d92b6d8b663cb0f4d2c3";
        usr.CountryId = 57;
        usr.DisplayMedals = 115587185;
        usr.Level = null;
        usr.Flags = 271679521;
        usr.RegionId = 0;
        usr.SubregionId = 0;

        var ent = new UnknownEntity();
        ent.User = usr;
        ent.Recorded = DateTime.Now;
        return ent;
    }

    private TimerCallback TimerCallbackFactory()
    {
        TimerCallback callback = async (object? obj) => {
            if (obj is not EsportalRequestHandler) return;
            var handler = (EsportalRequestHandler)obj;

            try
            {
                _currentUnknownEntity = await handler.GetNext();
                var next = _currentUnknownEntity;
                if (next is null) NoMoreItems();
                var result = await handler.HandleNext(next);
                await handler.FinalizeNext(next, result, null);
            }
            catch (System.Exception e)
            {
                if (!string.IsNullOrWhiteSpace(e.InnerException?.Message)) Console.WriteLine(e.InnerException?.Message);
                await handler.FinalizeNext(_currentUnknownEntity, null, e.Message);
            }
        };
        return callback;
    }
}