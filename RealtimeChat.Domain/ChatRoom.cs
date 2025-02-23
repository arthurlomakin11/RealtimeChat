namespace RealtimeChat.Domain;

public class ChatRoom
{
    public int Id { get; }
    public string Name { get; }
    
    private readonly List<MessageContent> _messages = [];
    public IReadOnlyCollection<MessageContent> Messages => _messages.AsReadOnly();
    
    private readonly List<ChatRoomParticipant> _participants = [];
    public IReadOnlyCollection<ChatRoomParticipant> Participants => _participants.AsReadOnly();

    public ChatRoom(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        Id = id;
        Name = name;
    }

    public void AddMessage(MessageContent message)
    {
        ArgumentNullException.ThrowIfNull(message);
        _messages.Add(message);
    }

    public void AddParticipant(ChatRoomParticipant participant)
    {
        ArgumentNullException.ThrowIfNull(participant);
        _participants.Add(participant);
    }
}