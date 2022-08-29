namespace Database;
public class Configuration
{
    public static Configuration Config;

    public string DatabaseUrl;

    private public Configuration()
    {
        var config =
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .Build();
        
        DatabaseUrl = config.DATABASE_URL;
    }
}
