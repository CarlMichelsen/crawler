using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class DataContext : DbContext
{
    public DataContext() : base()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new DatabaseConfiguration();
        optionsBuilder.UseSqlServer(config.ConnectionString);
    }

    public DbSet<ProfileEntity>? Profiles { get; set; }
    public DbSet<UserEntity>? Unknowns { get; set; }
}