using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

public class Friends
{
    [Key]
    public int Id { get; set; }


    public int UserId { get; set; }
    
    public User Friend { get; set; }
    
    public bool IsAccepted { get; set; }
    
}