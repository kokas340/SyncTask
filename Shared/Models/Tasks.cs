using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class Tasks
{
    [Key]
    public int Id { get; set; }
    public User Owner { get;  private set;  }
    [MaxLength(15)]
    public string Title { get;  private set;  }
  
    public string Description { get;  private set;  }
    public DateTime DueDate { get; set; }
  
    public Tasks(User owner, string title, string description)
    {
        Owner = owner;
        Title = title;
        Description = description;
    }
    private Tasks(){}
}