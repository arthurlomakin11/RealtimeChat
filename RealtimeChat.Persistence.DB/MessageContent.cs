namespace RealtimeChat.Persistence.DB;

public abstract record MessageContentEntity;

public record TextMessageContentEntity(string Text) : MessageContentEntity;
public record ImageMessageContentEntity(string Url, string? Caption) : MessageContentEntity;