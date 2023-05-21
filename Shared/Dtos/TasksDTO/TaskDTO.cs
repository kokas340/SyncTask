namespace Shared.Dtos;

public class TaskDTO
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string startDate { get; set; }
    public string endDate { get; set; }
    public int userId { get; set; }
    public bool group { get; set; }
}