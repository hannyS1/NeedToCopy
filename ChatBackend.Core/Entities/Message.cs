namespace ChatBackend.Core.Entities;

public class Message : BaseEntity
{
    public string Text { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }

    public User User { get; set; }
    public Room Room { get; set; }
}