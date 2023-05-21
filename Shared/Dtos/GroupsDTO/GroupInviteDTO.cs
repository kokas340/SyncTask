namespace Shared.Dtos;

public class GroupInviteDTO
{
    public int Id { get; set; }
    public GetUserDto User { get; set; }
    public GroupDTO Group { get; set; }
    public bool Accepted { get; set; }
}