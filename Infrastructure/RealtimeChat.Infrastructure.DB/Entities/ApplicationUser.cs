using Microsoft.AspNetCore.Identity;

namespace RealtimeChat.Persistence.DB.Entities;

public class ApplicationUser: IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public ICollection<MessageEntity> Messages { get; set; } = null!;
    public ICollection<ChatRoomParticipantEntity> ChannelParticipants { get; set; } = null!;
}