namespace WebCrawler;

public interface ICrawler<T>
{
    public Task<T?> Next();

    public Task<bool> Act(T? input);
}