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
            Age = 36,
            Email = "jack@via.dk",
            Domain = "via",
            Name = "Troels Mortensen",
            Password = "1234",
            Role = "Teacher",
            Username = "trmo",
            SecurityLevel = 4
        },
        new User
        {
            Age = 34,
            Email = "jakob@gmail.com",
            Domain = "gmail",
            Name = "Jakob Rasmussen",
            Password = "password",
            Role = "Student",
            Username = "jknr",
            SecurityLevel = 2
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
        
        
        if (string.IsNullOrEmpty(user.Email))
        {
            throw new ValidationException("Email cannot be null");
        }
        
        if (string.IsNullOrEmpty(user.Name))
        {
            throw new ValidationException("Name cannot be null");
        }
        
        users.Add(user);
        

        return Task.FromResult(user);
    }
}