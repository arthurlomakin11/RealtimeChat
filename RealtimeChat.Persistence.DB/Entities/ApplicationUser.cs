using Microsoft.AspNetCore.Identity;

namespace RealtimeChat.Persistence.DB.Entities;

public class ApplicationUser: IdentityUser
{
    public IReadOnlyCollection<MessageEntity> Messages { get; set; } = null!;
    public IReadOnlyCollection<ChatRoomParticipantEntity> ChannelParticipants { get; set; } = null!;
}