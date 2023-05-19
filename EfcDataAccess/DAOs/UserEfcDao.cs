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
        string hashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        User toCreate = new User
        {
            Username = dto.Username,
            Password = hashPassword,
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
        User user = await context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            return null;
        }
        return user;
    }

    public async Task<User> GetByIdAsync(int dtoFriendId)
    {
        User? existing = await context.Users.FindAsync(dtoFriendId);
       
        return existing;
        
    }
}