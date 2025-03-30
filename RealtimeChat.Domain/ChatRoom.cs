namespace RealtimeChat.Domain;

public class ChatRoom
{
    public int Id { get; set; } 
    public string Name { get; set; } = null!;
    
    public List<Message> Messages { get; set; } = null!;
    public List<ChatRoomParticipant> Participants { get; set; } = null!;
}