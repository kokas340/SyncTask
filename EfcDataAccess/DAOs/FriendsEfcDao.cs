using BlazorSyncTask.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Models;
using Friends = Shared.Models.Friends;

namespace EfcDataAccess.DAOs;


public class FriendsEfcDao
{
    private readonly AsyncTaskContext _context;

    public FriendsEfcDao(AsyncTaskContext dbContext)
    {
        this._context = dbContext;
    }


    public async Task<Friends> AddFriendAsync(int userId, GetUserDto friend)
    {
        // Check if friendship already exists
        bool friendshipExists = await _context.Friends
            .AnyAsync(f => f.UserId == userId && f.FriendId == friend.id || f.UserId == friend.id && f.FriendId == userId);
        if (friendshipExists)
        {
            throw new Exception("Friendship already exists.");
        }
        
        User friendToAdd = await _context.Users.FindAsync(friend.id);
        if (friendToAdd == null)
        {
            throw new Exception($"User with ID {friend.id} does not exist.");
        }

        Friends toCreate = new Friends()
        {
            UserId = userId,
            FriendId = friend.id,
            IsAccepted = false
        };

        EntityEntry<Friends> newFriend = await _context.Friends.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        return newFriend.Entity;
    }


    public async Task<List<GetUserDto>> GetFriendsAsync(int userId)
    {
        List<int> friendIds = await _context.Friends
            .Where(f => (f.UserId == userId || f.FriendId == userId) && f.IsAccepted)
            .Select(f => f.UserId == userId ? f.FriendId : f.UserId)
            .ToListAsync();

        List<User> friends = await _context.Users
            .Where(u => friendIds.Contains(u.Id))
            .ToListAsync();
        List<GetUserDto> friendDtos = friends.Select(f => new GetUserDto
        {
            id = f.Id,
            username = f.Username,
            fullName = f.FullName,
            email = f.Email,
            
        }).ToList();

        return friendDtos;
      

    }

    public async Task<List<GetFriendsDto>> GetAllPendingFriendsAsync(int userId)
    {
        List<int> friendIds = await _context.Friends
            .Where(f => f.FriendId == userId && !f.IsAccepted)
            .Select(f => f.UserId)
            .ToListAsync();
        
        List<User> friends = await _context.Users
            .Where(u => friendIds.Contains(u.Id))
            .ToListAsync();

        List<GetFriendsDto> friendDtos = new List<GetFriendsDto>();
        foreach (var friend in friends)
        {
            var friendRequest = await _context.Friends.FirstOrDefaultAsync(f => f.UserId == friend.Id && f.FriendId == userId);
            if (friendRequest == null)
            {
                throw new Exception($"No friend request found for user {friend.Id}");
            }
            var friendDto = new GetFriendsDto()
            {
                id = friend.Id,
                username = friend.Username,
                fullName = friend.FullName,
                email = friend.Email,
                friendRequstId = friendRequest.Id
            };
            friendDtos.Add(friendDto);
        }

        return friendDtos;
    }

    public async Task<List<GetUserDto>> GetAllUsers()
    {
        List<User> users = await _context.Users.ToListAsync();
        List<GetUserDto> friendDtos = users.Select(f => new GetUserDto
        {
            id = f.Id,
            username = f.Username,
            fullName = f.FullName,
            email = f.Email,
            
        }).ToList();
        return friendDtos;
    }

    public async Task<User> AcceptFriendRequest(int friendRequestId)
    {
        var friendRequest = await _context.Friends.FindAsync(friendRequestId);

        if (friendRequest == null)
        {
            throw new Exception($"Friend request with ID {friendRequestId} does not exist.");
        }

        if (friendRequest.IsAccepted)
        {
            throw new Exception($"Friend request with ID {friendRequestId} has already been accepted.");
        }

        friendRequest.IsAccepted = true;

        await _context.SaveChangesAsync();

        var friend = await _context.Users.FindAsync(friendRequest.FriendId);

        return friend;
    }
    
    public async Task DeleteFriend(int friendRequestId)
    {
        var friendRequest = await _context.Friends.FindAsync(friendRequestId);
        if (friendRequest == null)
        {
            throw new Exception($"Friend request with ID {friendRequestId} does not exist.");
        }
        
        if (friendRequest.IsAccepted)
        {
            throw new Exception($"Friend request with ID {friendRequestId} has already been accepted.");
        }
        
        _context.Friends.Remove(friendRequest);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFriend(int userId, int friendId)
    {
        var friendRequest = await _context.Friends
            .FirstOrDefaultAsync(f => (f.UserId == userId && f.FriendId == friendId) || 
                                      (f.UserId == friendId && f.FriendId == userId));

        if (friendRequest == null)
        {
            throw new Exception($"Friend relationship between user ID {userId} and friend ID {friendId} does not exist.");
        }
        _context.Friends.Remove(friendRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<GetUserDto> GetFriendAsync(int friendId)
    {
        User? friend = await _context.Users.FindAsync(friendId);
        if (friend == null)
        {
            throw new Exception($"User with ID {friendId} does not exist.");
        }
        var friendDto = new GetUserDto()
        {
            id = friend.Id,
            username = friend.Username,
            fullName = friend.FullName,
            email = friend.Email,
        };


        return friendDto;
    }
}