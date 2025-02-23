namespace RealtimeChat.Persistence.DB;

public class ChatRoomParticipantEntity
{
    public int Id { get; set; }
    public int ChatRoomParticipantId { get; set; }
    public int ChatRoomId { get; set; }
    public ChatRoomEntity ChatRoom { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}