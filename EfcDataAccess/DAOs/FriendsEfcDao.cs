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
        bool friendshipExists = await _context.friends
            .AnyAsync(f => f.user_id == userId && f.friend_id == friend.id || f.user_id == friend.id && f.friend_id == userId);
        if (friendshipExists)
        {
            throw new Exception("Friendship already exists.");
        }
        
        UserT friendToAdd = await _context.usert.FindAsync(friend.id);
        if (friendToAdd == null)
        {
            throw new Exception($"User with ID {friend.id} does not exist.");
        }

        Friends toCreate = new Friends()
        {
            user_id = userId,
            friend_id = friend.id,
            is_accepted = false
        };

        EntityEntry<Friends> newFriend = await _context.friends.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        return newFriend.Entity;
    }


    public async Task<List<GetUserDto>> GetFriendsAsync(int userId)
    {
        List<int> friendIds = await _context.friends
            .Where(f => (f.user_id == userId || f.friend_id == userId) && f.is_accepted)
            .Select(f => f.user_id == userId ? f.friend_id : f.user_id)
            .ToListAsync();

        List<UserT> friends = await _context.usert
            .Where(u => friendIds.Contains(u.id))
            .ToListAsync();
        List<GetUserDto> friendDtos = friends.Select(f => new GetUserDto
        {
            id = f.id,
            username = f.username,
            fullName = f.fullName,
            email = f.email,
            
        }).ToList();

        return friendDtos;
      

    }

    public async Task<List<GetFriendsDto>> GetAllPendingFriendsAsync(int userId)
    {
        List<int> friendIds = await _context.friends
            .Where(f => f.friend_id == userId && !f.is_accepted)
            .Select(f => f.user_id)
            .ToListAsync();
        
        List<UserT> friends = await _context.usert
            .Where(u => friendIds.Contains(u.id))
            .ToListAsync();

        List<GetFriendsDto> friendDtos = new List<GetFriendsDto>();
        foreach (var friend in friends)
        {
            var friendRequest = await _context.friends.FirstOrDefaultAsync(f => f.user_id == friend.id && f.friend_id == userId);
            if (friendRequest == null)
            {
                throw new Exception($"No friend request found for user {friend.id}");
            }
            var friendDto = new GetFriendsDto()
            {
                id = friend.id,
                username = friend.username,
                fullName = friend.fullName,
                email = friend.email,
                friendRequstId = friendRequest.id
            };
            friendDtos.Add(friendDto);
        }

        return friendDtos;
    }

    public async Task<List<GetUserDto>> GetAllUsers()
    {
        List<UserT> users = await _context.usert.ToListAsync();
        List<GetUserDto> friendDtos = users.Select(f => new GetUserDto
        {
            id = f.id,
            username = f.username,
            fullName = f.fullName,
            email = f.email,
            
        }).ToList();
        return friendDtos;
    }

    public async Task<UserT> AcceptFriendRequest(int friendRequestId)
    {
        var friendRequest = await _context.friends.FindAsync(friendRequestId);

        if (friendRequest == null)
        {
            throw new Exception($"Friend request with ID {friendRequestId} does not exist.");
        }

        if (friendRequest.is_accepted)
        {
            throw new Exception($"Friend request with ID {friendRequestId} has already been accepted.");
        }

        friendRequest.is_accepted = true;

        await _context.SaveChangesAsync();

        var friend = await _context.usert.FindAsync(friendRequest.friend_id);

        return friend;
    }
    
    public async Task DeleteFriend(int friendRequestId)
    {
        var friendRequest = await _context.friends.FindAsync(friendRequestId);
        if (friendRequest == null)
        {
            throw new Exception($"Friend request with ID {friendRequestId} does not exist.");
        }
        
        if (friendRequest.is_accepted)
        {
            throw new Exception($"Friend request with ID {friendRequestId} has already been accepted.");
        }
        
        _context.friends.Remove(friendRequest);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFriend(int userId, int friendId)
    {
        var friendRequest = await _context.friends
            .FirstOrDefaultAsync(f => (f.user_id == userId && f.friend_id == friendId) || 
                                      (f.user_id == friendId && f.friend_id == userId));

        if (friendRequest == null)
        {
            throw new Exception($"Friend relationship between user ID {userId} and friend ID {friendId} does not exist.");
        }
        _context.friends.Remove(friendRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<GetUserDto> GetFriendAsync(int friendId)
    {
        UserT? friend = await _context.usert.FindAsync(friendId);
        if (friend == null)
        {
            throw new Exception($"User with ID {friendId} does not exist.");
        }
        var friendDto = new GetUserDto()
        {
            id = friend.id,
            username = friend.username,
            fullName = friend.fullName,
            email = friend.email,
        };


        return friendDto;
    }
}