using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Models;


namespace EfcDataAccess.DAOs;

public class FriendsEfcDao
{
    private readonly AsyncTaskContext context;
    
    public FriendsEfcDao(AsyncTaskContext dbContext)
    {
        this.context = dbContext;
      
    }
    

    public async Task<Friends> AddFriendAsync(int user, User Friend, Boolean status)
    {
       
        Friends toCreate = new Friends()
        {
           UserId = user,
           Friend = Friend,
           IsAccepted = false
        };
        EntityEntry<Friends> newUser = await context.Friends.AddAsync(toCreate);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }
   
}