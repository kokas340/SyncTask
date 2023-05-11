using Shared.Models;

namespace BlazorSyncTask.Services;

public interface IFriendsInterface
{
    Task AddFriendAsync(int userId, int friendId);
    Task RemoveFriendAsync(int userId, int friendId);
    Task<Friends> GetFriendsAsync(int userId, int friendId);
    Task<List<User>> GetFriendsListAsync(int userId);
    Task<User> GetFriendByUsernameAsync(int userId, string username);
}