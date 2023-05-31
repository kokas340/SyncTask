namespace Shared.Dtos;

public class GroupDTO
{
    public int id { get; set; }
    public int owner { get; set; }
    public string groupName { get; set; }
    public bool accepted { get; set; }
    public List<TaskDTO> tasks { get; set; }
    public List<MemberDTO> members { get; set; }
    
    public bool IsOwner { get; set; }
}