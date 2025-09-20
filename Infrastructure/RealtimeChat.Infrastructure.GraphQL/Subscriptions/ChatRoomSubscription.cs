namespace RealtimeChat.Infrastructure.GraphQL;

public class MessageSubscription
{
    [Subscribe]
    [Topic("MessageUpdated")]
    public MessageUpdatedEvent OnMessageUpdated([EventMessage] MessageUpdatedEvent @event) => @event;
}