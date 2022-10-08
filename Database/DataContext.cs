using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Database;

public class DataContext : DbContext
{
    private readonly IDatabaseConfiguration _config;

    public DataContext(IDatabaseConfiguration config) : base()
    {
        _config = config;
        if (Database.CanConnect()) Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.DatabaseConnectionString, (SqlServerDbContextOptionsBuilder builder) => {
            //builder.EnableRetryOnFailure();
        });
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    public DbSet<ProfileEntity>? ProfileEntity { get; set; }
    public DbSet<UnknownEntity>? UnknownEntity { get; set; }
    public DbSet<UserEntity>? UserEntity { get; set; }
    public DbSet<FailedUnknownEntity>? FailedUnknownEntity { get; set; }
    public DbSet<ProfileConnectionEntity>? ProfileConnectionEntity { get; set; }
}