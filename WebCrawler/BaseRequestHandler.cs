namespace WebCrawler;

public abstract class BaseRequestHandler<T, R>
{
    public async Task<R?> HandleNext(T? next)
    {
        if (next is null) return default(R?);

        // get httpClient
        var client = ClientFactory();

        // send http request
        var request = new HttpRequestMessage(HttpMethod.Get, ToUri(next));
        var response = await client.SendAsync(request);

        // read response in stream
        return await HandleRequestResponse(response, next);
    }

    public abstract Uri ToUri(T input);

    public abstract Task<T?> GetNext();

    public abstract Task FinalizeNext(T? next, R? result); // if any one if these are null the operation should be considered a failure

    public abstract Task<R?> HandleRequestResponse(HttpResponseMessage response, T current);

    private HttpClient ClientFactory()
    {
        var client = new HttpClient();
        return client;
    }
}