namespace RealtimeChat.Persistence.DB.Entities;

public class ChatRoomParticipantEntity
{
    public int Id { get; set; }
    public int ChatRoomId { get; set; }
    public ChatRoomEntity ChatRoom { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}