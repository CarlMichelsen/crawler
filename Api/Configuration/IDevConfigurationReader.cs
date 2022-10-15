namespace Api.Configuration;

public interface IDevConfigurationReader
{
    Dictionary<string, string?> Configuration { get; }
}