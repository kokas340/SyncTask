using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class Tasks
{
    [Key]
    public int id { get; set; }
    public UserT owner { get;  private set;  }
    [MaxLength(15)]
    public string title { get;  private set;  }
  
    public string description { get;  private set;  }
    public DateTime endDate { get; set; }
    public DateTime startDate { get; set; }
    public Tasks(UserT owner, string title, string description)
    {
        this.owner = owner;
        this.title = title;
        this.description = description;
    }
    private Tasks(){}
}