namespace RealtimeChat.Domain;

public class ChatRoomParticipant
{
    public int Id { get; }
    public int ChatRoomId { get; }
    public string UserId { get; }

    public ChatRoomParticipant(int id, int chatRoomId, string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId required", nameof(userId));
        Id = id;
        ChatRoomId = chatRoomId;
        UserId = userId;
    }
}