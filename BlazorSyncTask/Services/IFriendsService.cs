using Shared.Models;

namespace BlazorSyncTask.Services;

public interface IFriendsService
{
    Task<List<User>> getAllFriends(int userId);
    User getFriendId(int friendId);

}