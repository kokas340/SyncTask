using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Shared.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{

    private readonly IList<User> users = new List<User>
    {
        new User
        {
            
            FullName = "Troels Mortensen",
            Password = "1234",
            
            Username = "trmo",
            
        },
        new User
        {
            FullName = "kris",
            Password = "password",
           
            Username = "jknr",
           
        }
    };

    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task<User> RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        
        
        users.Add(user);
        

        return Task.FromResult(user);
    }
}