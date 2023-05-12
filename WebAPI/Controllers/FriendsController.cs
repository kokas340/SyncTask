using BlazorSyncTask.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FriendsController : ControllerBase
{
    private readonly IFriendsInterface friendsService;

    public FriendsController(IFriendsInterface friendsService)
    {
        this.friendsService = friendsService;
    }

    [HttpPost("{userId}/addFriend")]
    public async Task<ActionResult> AddFriend(int userId, int friendId)
    {
        await friendsService.AddFriendAsync(userId, friendId);
        return Ok();
    }

    [HttpDelete("{userId}/deleteFriend")]
    public async Task<ActionResult> RemoveFriend(int userId, int friendId)
    {
        await friendsService.RemoveFriendAsync(userId, friendId);
        return Ok();
    }

    [HttpGet("{userId}/getFriend")]
    public async Task<ActionResult<Friends>> GetFriend(int userId, int friendId)
    {
        var friends = await friendsService.GetFriendsAsync(userId, friendId);
        if (friends == null)
        {
            return NotFound();
        }
        return friends;
    }

    [HttpGet("{userId}/friends")]
    public async Task<ActionResult<List<User>>> GetFriendsList(int userId)
    {
        var friendsList = await friendsService.GetFriendsListAsync(userId);
        return friendsList;
    }

    [HttpGet("{userId}/getFriendByUsername")]
    public async Task<ActionResult<User>> GetFriendByUsername(int userId, string username)
    {
        var friend = await friendsService.GetFriendByUsernameAsync(userId, username);
        if (friend == null)
        {
            return NotFound();
        }
        return friend;
    }
}