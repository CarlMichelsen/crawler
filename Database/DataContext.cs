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
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
    }

    public DbSet<ProfileEntity>? Profiles { get; set; }
}