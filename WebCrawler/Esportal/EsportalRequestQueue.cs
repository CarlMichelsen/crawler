using WebCrawler.Esportal.Model;

namespace WebCrawler.Esportal;

public class EsportalRequestQueue : BaseRequestQueue
{
    private Uri EsportalUri(ProfileRequestConfig config) {
        var protocol = "https";
        var host = "esportal.com";
        var path = "api/user_profile/get";
        return new Uri($"{protocol}://{host}/{path}?{config.Query()}");
    }

    public override async Task<Uri?> GetNext()
    {
        await Task.Delay(10);
        return EsportalUri(ProfileRequestConfig.AllTrue(113878688));
    }

    public override void FinalizeNext(bool success)
    {
        if (success)
        {
            Console.WriteLine("Success!");
        }
        else
        {
            Console.WriteLine("Failure!");
        }
    }
}