namespace Database;

public class DatabaseConfiguration
{
    public string ConnectionString { get; }

    public DatabaseConfiguration()
    {
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        ConnectionString = databaseUrl ?? "Server=localhost\\SQLEXPRESS;Database=esportal;Trusted_Connection=True;";
    }
}