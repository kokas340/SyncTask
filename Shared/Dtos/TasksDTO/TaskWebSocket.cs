namespace Shared.Dtos;

public class TaskWebSocket
{
    public long id{ get; set; }
    public String name{ get; set; }
    public String description{ get; set; }
    public String startDate{ get; set; }
    public String endDate{ get; set; }
    public bool isGroup{ get; set; }
    public long userId{ get; set; }
    public long? groupId{ get; set; }
}