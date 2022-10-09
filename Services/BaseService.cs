namespace Services;

public abstract class BaseService
{
    private protected static string ToQueryString(Dictionary<string, string> keyValues)
    {
        var stringItems = keyValues.Select(kv => $"{System.Web.HttpUtility.UrlEncode(kv.Key)}={System.Web.HttpUtility.UrlEncode(kv.Value)}");
        return $"{string.Join("&", stringItems)}";
    }
}