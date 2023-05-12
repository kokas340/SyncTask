using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class UserEfcDao
{
    private readonly AsyncTaskContext context;
    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        return user;
    }
    public UserEfcDao(AsyncTaskContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(UserRegisterDto dto)
    {
       
        if (await context.Users.AnyAsync(u => u.Username == dto.Username))
        {
            throw new Exception("A user with this username already exists.");
        }
        if (await context.Users.AnyAsync(u => u.Email == dto.Email))
        {
            throw new Exception("A user with this email already exists.");
        }
        if (!IsValidEmail(dto.Email))
        {
            throw new Exception("The email address is not valid.");
        }
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
    private bool IsValidEmail(string email)
    {
        // Regular expression pattern for validating email addresses
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(email);
    }
  
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
     
        User user = await context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username && u.Password == dto.Password);
        if (user == null)
        {
            return null;
        }
        return user;
    }
    
   
}