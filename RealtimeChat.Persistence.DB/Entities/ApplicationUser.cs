using Microsoft.AspNetCore.Identity;

namespace RealtimeChat.Persistence.DB.Entities;

public class ApplicationUser: IdentityUser
{
    public ICollection<MessageEntity> Messages { get; set; } = null!;
    public ICollection<ChatRoomParticipantEntity> ChannelParticipants { get; set; } = null!;
}