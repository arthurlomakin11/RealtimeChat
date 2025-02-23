namespace RealtimeChat.Persistence.GraphQL;

public class ImageMessageContentGraph : IMessageContentGraph
{
    public string Url { get; set; } = string.Empty;
    public string? Caption { get; set; }
}