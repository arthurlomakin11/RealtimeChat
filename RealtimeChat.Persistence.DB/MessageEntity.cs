namespace RealtimeChat.Persistence.DB;

public class MessageEntity
{
    public int Id { get; set; }
    public required MessageContentEntity Content { get; set; }
    public DateTime SentAt { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    public int ChatRoomId { get; set; }
    public ChatRoomEntity ChatRoom { get; set; } = null!;
}