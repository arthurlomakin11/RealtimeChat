namespace RealtimeChat.Persistence.GraphQL;

public class ChatRoomGraph
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<MessageGraph> Messages { get; set; } = [];
}