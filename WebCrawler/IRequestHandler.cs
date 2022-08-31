namespace WebCrawler;

public interface IRequestHandler<T, R>
{
    public abstract Task<R?> HandleNext(T? next);

    public abstract Task<T?> GetNext();

    public abstract Task<bool> FinalizeNext(T? next, R? result, string? errorMessage); // if any one if these are null the operation should be considered a failure

    public abstract Task<R?> HandleRequestResponse(HttpResponseMessage response, T current);

    public abstract Uri ToUri(T input);

    public abstract HttpClient ClientFactory();
}