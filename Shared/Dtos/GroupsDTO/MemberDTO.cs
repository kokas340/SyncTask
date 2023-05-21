namespace Shared.Dtos;

public class MemberDTO
{
    public int Id { get; set; }
    public GetUserDto User { get; set; }
    public bool Accepted { get; set; }
}