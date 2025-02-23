namespace RealtimeChat.Domain;

public abstract record MessageContent;

public record TextMessageContent(string Text) : MessageContent;

public record ImageMessageContent(string Url, string? Caption) : MessageContent;