using BlazorSyncTask.Services;
using EfcDataAccess;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace WebAPI.Services;

public class FriendsService : IFriendsService
{
    private readonly AsyncTaskContext dbContext;

    public FriendsService(AsyncTaskContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Friends> AddFriendAsync(Friends friends)
    {
        await dbContext.Friends.AddAsync(friends);
        await dbContext.SaveChangesAsync();
        return friends;
    }

    public async Task<Friends> GetFriendsAsync(int userId, int friendId)
    {
        return await dbContext.Friends
            .Include(f => f.User)
            .Include(f => f.Friend)
            .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);
    }

    public async Task<List<User>> GetFriendsListAsync(int userId)
    {
        return await dbContext.Friends
            .Include(f => f.Friend)
            .Where(f => f.UserId == userId)
            .Select(f => f.Friend)
            .ToListAsync();
    }

    public async Task RemoveFriendAsync(Friends friends)
    {
        dbContext.Friends.Remove(friends);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Friends> UpdateFriendAsync(Friends friends)
    {
        dbContext.Entry(friends).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return friends;
    }

    public async Task<User> GetFriendByUsernameAsync(string username)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}