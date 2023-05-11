using Shared.Models;

namespace WebAPI.Services;

public interface IFriendsService
{
    Task<Friends> AddFriendAsync(Friends friends);
    Task RemoveFriendAsync(Friends friends);
    Task<Friends> GetFriendsAsync(int userId, int friendId);
    Task<List<User>> GetFriendsListAsync(int userId);
    Task<User> GetFriendByUsernameAsync(string username);
}