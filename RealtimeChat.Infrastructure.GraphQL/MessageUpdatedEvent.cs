using RealtimeChat.Persistence.GraphQL;

namespace RealtimeChat.Infrastructure.GraphQL;

public class MessageUpdatedEvent
{
    public string EventType { get; set; } = null!;
    public MessageGraph? Message { get; set; }
}