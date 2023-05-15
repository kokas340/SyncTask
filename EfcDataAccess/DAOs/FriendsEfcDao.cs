using BlazorSyncTask.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Models;
using Friends = Shared.Models.Friends;

namespace EfcDataAccess.DAOs;


public class FriendsEfcDao
{
    private readonly AsyncTaskContext context;

    public FriendsEfcDao(AsyncTaskContext dbContext)
    {
        this.context = dbContext;
    }


    public async Task<Friends> AddFriendAsync(int user, GetUserDto friend)
    {
        Friends toCreate = new Friends()
        {
            UserId = user,
            Friend = friend,
            IsAccepted = false
        };

        EntityEntry<Friends> newFriend = await context.Friends.AddAsync(toCreate);
        await context.SaveChangesAsync();
        return newFriend.Entity;
    }


    public async Task<List<GetUserDto>> GetFriendsAsync(int userId)
    {
        List<int> friendIds = await context.Friends
            .Where(f => (f.UserId == userId || f.FriendId == userId) && f.IsAccepted)
            .Select(f => f.UserId == userId ? f.FriendId : f.UserId)
            .ToListAsync();

        List<User> friends = await context.Users
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

    public async Task<List<GetUserDto>> getAllPendingFriendsAsync(int userId)
    {
        List<int> friendIds = await context.Friends
            .Where(f => f.FriendId == userId && !f.IsAccepted)
            .Select(f => f.UserId)
            .ToListAsync();
        
        List<User> friends = await context.Users
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

    public async Task<List<GetUserDto>> getAllUsers()
    {
        List<User> users = await context.Users.ToListAsync();
        List<GetUserDto> friendDtos = users.Select(f => new GetUserDto
        {
            id = f.Id,
            username = f.Username,
            fullName = f.FullName,
            email = f.Email,
            
        }).ToList();
        return friendDtos;
    }
}