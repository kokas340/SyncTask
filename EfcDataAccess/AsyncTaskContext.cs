using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess;


public class AsyncTaskContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<Friends> Friends { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      
      
        string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12344;SearchPath=synctask";
        optionsBuilder.UseNpgsql(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }
}