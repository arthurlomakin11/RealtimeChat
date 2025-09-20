namespace RealtimeChat.Persistence.GraphQL;

public class ImageMessageContentGraph : IMessageContent
{
    public required string Url { get; set; }
    public string? Caption { get; set; }
}