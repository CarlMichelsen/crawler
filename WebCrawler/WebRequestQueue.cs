namespace WebCrawler;

public class WebRequestQueue : IWebRequestQueue
{
    private Queue<Uri> _queue;

    public WebRequestQueue(IEnumerable<string> list)
    {
        _queue = new Queue<Uri>(list.Select(s => String2Uri(s)));
    }

    public WebRequestQueue(IEnumerable<Uri> list)
    {
        _queue = new Queue<Uri>(list.Select(u => UriPipeline(u)));
    }

    public WebRequestQueue(string first)
    {
        _queue = new Queue<Uri>();
        _queue.Append(String2Uri(first));
    }

    public WebRequestQueue(Uri first)
    {
        _queue = new Queue<Uri>();
        _queue.Append(UriPipeline(first));
    }

    public WebRequestQueue()
    {
        _queue = new Queue<Uri>();
    }

    public bool Push(Uri input)
    {
        try
        {
            var uri = UriPipeline(input);
            AppendUri(uri);
        }
        catch (System.Exception)
        {
            return false;
        }
        return true;
    }

    public bool Push(string input)
    {
        try
        {
            var uri = String2Uri(input);
            return Push(uri);
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public async Task<bool> Next(IWebRequestQueue.HandleQueueItem handler)
    {
        Uri? queueItem = _queue.Dequeue();
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

            // re-add uri and handle it later
            if (!success) _queue.Append(queueItem);
        }

        return success;
    }

    public int Length {
        get { return _queue.Count(); }
    }

    private HttpClient ClientFactory()
    {
        return new HttpClient();
    }

    private Uri UriPipeline(Uri input)
    {
        return input;
    }

    private Uri String2Uri(string input)
    {
        return UriPipeline(new Uri(input));
    }

    private void AppendUri(Uri input)
    {
        _queue.Enqueue(input);
    }
}