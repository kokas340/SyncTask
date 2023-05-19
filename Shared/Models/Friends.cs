using System.ComponentModel.DataAnnotations;
using Shared.Dtos;

namespace Shared.Models;


public class Friends
{
    [Key]
    public int id { get; set; }
    
    public int user_id { get; set; }
    public int friend_id { get; set; }
   
    
    public bool is_accepted { get; set; }
    
}