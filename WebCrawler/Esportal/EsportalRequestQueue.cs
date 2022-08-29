using WebCrawler.Esportal.Model;

namespace WebCrawler.Esportal;

public class EsportalRequestQueue : BaseRequestQueue
{
    public Queue<long> IdQueue;
    private long _currentId = -1;

    public EsportalRequestQueue()
    {
        IdQueue = new Queue<long>();
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
        return EsportalUri(ProfileRequestConfig.AllTrue(_currentId));
    }

    public override void FinalizeNext(bool success)
    {
        if (success)
        {
            Console.WriteLine("Success!");
            _currentId = -1;
        }
        else
        {
            if (_currentId != (long)-1) IdQueue.Enqueue(_currentId);
            Console.WriteLine("Failure!");
        }
    }
}