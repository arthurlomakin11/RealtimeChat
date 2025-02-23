namespace RealtimeChat.Persistence.DB;

public class ApplicationUser: IdentityUser
{
    public IReadOnlyCollection<MessageEntity> Messages { get; set; } = [];
    public IReadOnlyCollection<ChatRoomParticipantEntity> ChannelParticipants { get; set; } = [];
}