namespace Database;

public class DatabaseConfiguration
{
    public string ConnectionString { get; }

    public DatabaseConfiguration()
    {
        ConnectionString = "Server=localhost\\SQLEXPRESS;Database=esportalnew;Trusted_Connection=True;";
    }
}