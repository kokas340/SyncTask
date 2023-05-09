using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class FriendsEfcDao
{
    private readonly AsyncTaskContext dbContext;

    public FriendsEfcDao(AsyncTaskContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddFriendAsync(Friends friends)
    {
        await dbContext.Set<Friends>().AddAsync(friends);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveFriendAsync(Friends friends)
    {
        dbContext.Set<Friends>().Remove(friends);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Friends?> GetFriendsAsync(int userId, int friendId)
    {
        return await dbContext.Set<Friends>()
            .Include(f => f.User)
            .Include(f => f.Friend)
            .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);
    }

    public async Task<List<User>> GetFriendsListAsync(int userId)
    {
        return await dbContext.Set<Friends>()
            .Include(f => f.Friend)
            .Where(f => f.UserId == userId)
            .Select(f => f.Friend)
            .ToListAsync();
    }
    
    public async Task<User?> GetFriendByUsernameAsync(int userId, string username)
    {
        return await dbContext.Set<Friends>()
            .Include(f => f.Friend)
            .Where(f => f.UserId == userId && f.Friend.Username == username)
            .Select(f => f.Friend)
            .FirstOrDefaultAsync();
    }
}