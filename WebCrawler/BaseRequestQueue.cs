namespace WebCrawler;

public abstract class BaseRequestQueue : IRequestQueue
{
    public async Task<bool> HandleNext(IRequestQueue.HandleQueueItem handler)
    {
        // get next queue item or return
        Uri? queueItem = await GetNext();
        if (queueItem is null) return false;

        // get httpClient
        var client = ClientFactory();

        // send http request
        var request = new HttpRequestMessage(HttpMethod.Get, queueItem);
        var response = await client.SendAsync(request);

        // read response in stream
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

        // handle list item depending on success
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