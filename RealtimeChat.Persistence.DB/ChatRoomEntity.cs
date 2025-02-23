namespace RealtimeChat.Persistence.DB;

using System.Collections.Generic;

public class ChatRoomEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<MessageEntity> Messages { get; set; } = null!;
    public ICollection<ChatRoomParticipantEntity> ChatRoomParticipants { get; set; } = null!;
}