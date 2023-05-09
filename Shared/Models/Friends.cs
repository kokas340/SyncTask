using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;
[Table("Friends")]
public class Friends
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int RelationId { get; set; }

    public int UserId { get; set; }
    
    public User User { get; set; } 

    public int FriendId { get; set; }
    public User Friend { get; set; }
}