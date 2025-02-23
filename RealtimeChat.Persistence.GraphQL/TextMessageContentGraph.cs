namespace RealtimeChat.Persistence.GraphQL;

public class TextMessageContentGraph : IMessageContentGraph
{
    public string Text { get; set; } = string.Empty;
}