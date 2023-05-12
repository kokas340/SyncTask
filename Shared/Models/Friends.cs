using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;
[Table("Friends")]
public class Friends
{
    [Key]
    public int RelationId { get; set; }
    public User FriendSender { get; set; }
    public User FriendReceiver { get; set; }
    public string status{ get; set; }
}