using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Models;
using BCrypt.Net;

namespace EfcDataAccess.DAOs;
public class UserEfcDao
{
    private readonly AsyncTaskContext context;

    public UserEfcDao(AsyncTaskContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(UserRegisterDto dto)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        User toCreate = new User
        {
            Username = dto.Username,
            Password = hashedPassword,
            Email = dto.Email,
            FullName = dto.FullName,
        };

        EntityEntry<User> newUser = await context.Users.AddAsync(toCreate);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    //public Task<IEnumerable<User>> GetUser(SearchTodoParametersDto searchParameters)
    //{
    //     throw new NotImplementedException();
    // }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByUsername(string userName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }

    public Task DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> LoginAsync(UserLoginDto dto)
    {
        // Find the user by the specified username
        User user = await context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        // If no user is found or the password doesn't match, return null to indicate that login failed
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            return null;
        }
        // Password is correct, return the user object to indicate successful login
        return user;
    }

    public async Task<User> GetByIdAsync(int dtoFriendId)
    {
        User? existing = await context.Users.FindAsync(dtoFriendId);
       
        return existing;
        
    }
}