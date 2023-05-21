namespace Shared.Dtos;

public class CreateTaskDto
{

    public long userId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string startDate { get; set; }
    public string endDate { get; set; }
    
}


