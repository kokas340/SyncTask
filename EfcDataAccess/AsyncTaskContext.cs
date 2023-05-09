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
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/AsyncTask.db");
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //overriding model
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        
        modelBuilder.Entity<Friends>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Friends>()
            .HasOne(p => p.Friend)
            .WithMany()
            .HasForeignKey(p => p.FriendId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}