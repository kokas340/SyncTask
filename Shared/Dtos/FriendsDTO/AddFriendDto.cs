namespace Shared.Dtos;

public class AddFriendDto
{

    
    public int user_id { get; init; }
    
    public int friend_id { get; init; }
    
    public bool is_accepted { get; init; }
}