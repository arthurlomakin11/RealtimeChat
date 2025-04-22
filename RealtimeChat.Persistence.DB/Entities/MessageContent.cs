using System.Text.Json.Serialization;

namespace RealtimeChat.Persistence.DB.Entities;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(TextMessageContentEntity), "text")]
[JsonDerivedType(typeof(ImageMessageContentEntity), "image")]
public abstract record MessageContentEntity;

public record TextMessageContentEntity(string Text) : MessageContentEntity;
public record ImageMessageContentEntity(string Url, string? Caption) : MessageContentEntity;