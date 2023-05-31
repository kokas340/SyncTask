using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess;


public class AsyncTaskContext: DbContext
{
    public DbSet<UserT> usert { get; set; }
    public DbSet<Tasks> task { get; set; }
    public DbSet<Friends> friends { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      
      
        string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;SearchPath=synctask";
        optionsBuilder.UseNpgsql(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }
}