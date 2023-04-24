using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

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
        User toCreate = new User
        {
            Username = dto.Username,
            Password = dto.Password,
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
}
