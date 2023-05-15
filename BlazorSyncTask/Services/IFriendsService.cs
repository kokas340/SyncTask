using Shared.Dtos;
using Shared.Models;

namespace BlazorSyncTask.Services;

public interface IFriendsService
{
    Task<List<GetUserDto>> GetAllFriends(int userId);
    Task AddFriend(int userId, int friendId);
    User GetFriendId(int friendId);

    Task<List<GetUserDto>> GetAllUsers();
    Task<List<GetFriendsDto>> GetAllFriendsPending(int toInt32);
    Task AcceptPending(int requestId);
    Task DeletePending(int requestId);
}