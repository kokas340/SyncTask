using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [MaxLength(15)]
    public string Username { get; set; }
    [MinLength(6)]
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public ICollection<Tasks> Tasks { get; set; }
    [JsonIgnore]
    public ICollection<User> Friends { get; set; }
}