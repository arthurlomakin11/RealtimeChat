namespace RealtimeChat.Domain;

public class ChatRoom
{
    public int Id { get; }
    public string Name { get; }
    
    private readonly List<MessageContent> _messages = [];
    public IReadOnlyCollection<MessageContent> Messages => _messages;
    
    private readonly List<ChatRoomParticipant> _participants = [];
    public IReadOnlyCollection<ChatRoomParticipant> Participants => _participants;

    public ChatRoom(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        Id = id;
        Name = name;
    }

    public ChatRoom AddMessage(MessageContent message)
    {
        ArgumentNullException.ThrowIfNull(message);
        _messages.Add(message);
        return this;
    }
    
    public ChatRoom AddMessages(IEnumerable<MessageContent> messages)
    {
        ArgumentNullException.ThrowIfNull(messages);
        _messages.AddRange(messages);
        return this;
    }

    public void AddParticipant(ChatRoomParticipant participant)
    {
        ArgumentNullException.ThrowIfNull(participant);
        _participants.Add(participant);
    }
}