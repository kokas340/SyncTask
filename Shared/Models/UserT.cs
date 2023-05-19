using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class UserT
{
    [Key]
    public int id { get; set; }
    [MaxLength(15)]
    public string username { get; set; }
    [MinLength(6)]
    public string password { get; set; }
    public string fullName { get; set; }
    public string email { get; set; }
  
    public ICollection<Tasks> Tasks { get; set; }

}