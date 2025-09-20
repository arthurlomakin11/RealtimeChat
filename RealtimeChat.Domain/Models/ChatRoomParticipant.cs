namespace RealtimeChat.Domain.Models;

public class ChatRoomParticipant
{
    public int Id { get; }
    public int ChatRoomId { get; }
    public string UserId { get; } = null!;
}