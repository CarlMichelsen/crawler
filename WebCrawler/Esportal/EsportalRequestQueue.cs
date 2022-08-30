using WebCrawler.Esportal.Model;

namespace WebCrawler.Esportal;

public class EsportalRequestQueue : BaseRequestQueue
{
    public Queue<ulong> IdQueue;
    private ulong? _currentId = null;

    public EsportalRequestQueue()
    {
        IdQueue = new Queue<ulong>();
        IdQueue.Enqueue(113878688);
    }

    private Uri EsportalUri(ProfileRequestConfig config) {
        var protocol = "https";
        var host = "esportal.com";
        var path = "api/user_profile/get";
        return new Uri($"{protocol}://{host}/{path}?{config.Query()}");
    }

    public override async Task<Uri?> GetNext()
    {
        await Task.Delay(10);
        _currentId = IdQueue.Dequeue();
        if (_currentId is null) return null;
        return EsportalUri(ProfileRequestConfig.AllTrue((ulong)_currentId));
    }

    public override void FinalizeNext(bool success)
    {
        if (success)
        {
            Console.WriteLine("Success!");
            _currentId = null;
        }
        else
        {
            if (_currentId is not null) IdQueue.Enqueue((ulong)_currentId);
            Console.WriteLine("Failure!");
        }
    }
}