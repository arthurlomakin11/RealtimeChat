namespace RealtimeChat.Persistence.DB.Entities;

using System.Text.Json.Serialization;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(MessageContentEntity))]
[JsonSerializable(typeof(TextMessageContentEntity))]
[JsonSerializable(typeof(ImageMessageContentEntity))]
[JsonSerializable(typeof(string))]
public partial class MessageJsonContext : JsonSerializerContext;