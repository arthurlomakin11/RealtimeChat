namespace RealtimeChat.Persistence.GraphQL;

public class MessageGraph
{
    public int Id { get; set; }
    public DateTime SentAt { get; set; }
    public string SenderId { get; set; } = string.Empty;
    public int ChatRoomId { get; set; }
    public IMessageContent Content { get; set; } = null!;
}