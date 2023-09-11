namespace ChatBackend.Core.Entities;

public class UserRoom : BaseEntity
{
    public int UserId { get; set; }
    public int RoomId { get; set; }
    
    public User User { get; set; }
    public Room Room { get; set; }
}