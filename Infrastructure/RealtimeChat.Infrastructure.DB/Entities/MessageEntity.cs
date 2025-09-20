using RealtimeChat.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChat.Persistence.DB.Entities;

public class MessageEntity
{
    public int Id { get; set; }
    public required MessageContentEntity Content { get; set; }
    
    [NotMapped]
    public string ContentJson => Content.ToJson(JsonSettings.MessageContentJsonSettings);
    public DateTime SentAt { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    public int ChatRoomId { get; set; }
    public ChatRoomEntity ChatRoom { get; set; } = null!;
}