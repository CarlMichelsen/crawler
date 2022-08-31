using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Database;

public class DataContext : DbContext
{
    public DataContext() : base()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new DatabaseConfiguration();
        optionsBuilder.UseSqlServer(config.ConnectionString, (SqlServerDbContextOptionsBuilder builder) => {
            builder.EnableRetryOnFailure();
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
}