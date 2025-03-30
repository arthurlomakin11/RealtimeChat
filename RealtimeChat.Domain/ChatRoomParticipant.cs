namespace RealtimeChat.Domain;

public class ChatRoomParticipant
{
    public int Id { get; }
    public int ChatRoomId { get; }
    public string UserId { get; }
}