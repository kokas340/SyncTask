using System.IdentityModel.Tokens.Jwt;
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
        string hashPassword = BCrypt.Net.BCrypt.HashPassword(dto.password);

        UserT toCreate = new UserT
        {
            username = dto.username,
            password = hashPassword,
            email = dto.email,
            fullname = dto.fullName,
        };

        EntityEntry<UserT> newUser = await context.usert.AddAsync(toCreate);
        await context.SaveChangesAsync();
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
        UserT user = await context.usert.FirstOrDefaultAsync(u => u.username == dto.username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.password, user.password))
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

    public string GetIdByToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;
        var userId = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;
        return userId;
    }
}