namespace WebCrawler;

public abstract class BaseRequestQueue : IRequestQueue
{
    public async Task<bool> HandleNext(IRequestQueue.HandleQueueItem handler)
    {
        Uri? queueItem = await GetNext();
        if (queueItem is null) return false;
        var client = ClientFactory();

        var request = new HttpRequestMessage(HttpMethod.Get, queueItem);
        var response = await client.SendAsync(request);

        var success = false;
        if (response.IsSuccessStatusCode)
        {
            var contentStream = response.Content.ReadAsStream();
            using (var reader = new StreamReader(contentStream))
            {
                var content = await reader.ReadToEndAsync();
                success = await handler(content, response.Headers);
            }
        }

        FinalizeNext(success);
        return success;
    }

    public abstract Task<Uri?> GetNext();

    public abstract void FinalizeNext(bool success);

    private HttpClient ClientFactory()
    {
        var client = new HttpClient();
        return client;
    }
}