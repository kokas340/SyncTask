using Shared.Models;

namespace Shared.Dtos;

public class AddFriendDto
{

    
    public int UserId { get; init; }
    
    public int FriendId { get; init; }
    
    public bool IsAccepted { get; init; }
}