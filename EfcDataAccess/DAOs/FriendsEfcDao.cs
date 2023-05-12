using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;


namespace EfcDataAccess.DAOs;

public class FriendsEfcDao
{
    private readonly AsyncTaskContext context;

    public FriendsEfcDao(AsyncTaskContext dbContext)
    {
        this.context = dbContext;
    }


    public async Task<Friends> CreateAsync(Friends todo)
    {
        EntityEntry<Friends> added = await context.Friends.AddAsync(todo);
        await context.SaveChangesAsync();
        return added.Entity;
    }
}