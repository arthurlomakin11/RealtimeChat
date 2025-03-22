namespace RealtimeChat.Domain;

public class Message
{
    public int Id { get; set; }
    public DateTime SentAt { get; set; }
    public string SenderId { get; set; } = null!;
    public int ChatRoomId { get; set; }
    public MessageContent Content { get; set; } = null!;
}