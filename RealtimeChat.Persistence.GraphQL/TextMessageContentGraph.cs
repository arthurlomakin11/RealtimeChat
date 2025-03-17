namespace RealtimeChat.Persistence.GraphQL;

public class TextMessageContentGraph : IMessageContent
{
    public required string Text { get; set; }
}