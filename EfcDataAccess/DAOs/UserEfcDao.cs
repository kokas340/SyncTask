using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Models;

namespace EfcDataAccess.DAOs;
public class UserEfcDao
{
    private readonly AsyncTaskContext context;

    public UserEfcDao(AsyncTaskContext context)
    {
        this.context = context;
    }
    public async Task<UserT> CreateAsync(UserRegisterDto dto)
    {
        UserT toCreate = new UserT
        {
            username = dto.username,
            password = dto.password,
            email = dto.email,
            fullName = dto.fullName,
        };
        Console.WriteLine("TEST 111111");
        EntityEntry<UserT> newUser = await context.usert.AddAsync(toCreate);
        Console.WriteLine("TEST 222222");
        await context.SaveChangesAsync();
        Console.WriteLine("TEST 3333333");
        return newUser.Entity;
    }

    //public Task<IEnumerable<User>> GetUser(SearchTodoParametersDto searchParameters)
    //{
    //     throw new NotImplementedException();
    // }

    public Task UpdateUser(UserT user)
    {
        throw new NotImplementedException();
    }

    public async Task<UserT?> GetByUsername(string userName)
    {
        UserT? existing = await context.usert.FirstOrDefaultAsync(u =>
            u.username.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }

    public Task DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserT> LoginAsync(UserLoginDto dto)
    {
        // Search for a user with the specified username and password
        UserT user = await context.usert.FirstOrDefaultAsync(u => u.username == dto.username && u.password == dto.password);
    
        // If no user is found, return null to indicate that login failed
        if (user == null)
        {
            return null;
        }
    
        // Otherwise, return the user object to indicate successful login
        return user;
    }

    public async Task<UserT> GetByIdAsync(int dtoFriendId)
    {
        UserT? existing = await context.usert.FindAsync(dtoFriendId);
       
        return existing;
        
    }
}