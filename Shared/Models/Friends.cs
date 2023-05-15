using System.ComponentModel.DataAnnotations;
using Shared.Dtos;

namespace Shared.Models;


public class Friends
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public int FriendId { get; set; }
    public GetUserDto Friend { get; set; }
    
    public bool IsAccepted { get; set; }
    
}