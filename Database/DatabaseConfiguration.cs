namespace Database;

public class DatabaseConfiguration
{
    public string ConnectionString { get; }

    public DatabaseConfiguration()
    {
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        ConnectionString = databaseUrl ?? "Server=localhost,1433\\SQLEXPRESS;Database=esportal;MultipleActiveResultSets=True;User Id=SA;Password=ThisIsATestDevelopmentPassword!123;";
    }
}