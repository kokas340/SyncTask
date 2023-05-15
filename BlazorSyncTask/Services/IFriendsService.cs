using Shared.Dtos;
using Shared.Models;

namespace BlazorSyncTask.Services;

public interface IFriendsService
{
    Task<List<GetUserDto>> GetAllFriends(int userId);
    User GetFriendId(int friendId);

}