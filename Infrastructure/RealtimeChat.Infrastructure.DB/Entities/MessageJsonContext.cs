using System.Text.Json.Serialization;

namespace RealtimeChat.Persistence.DB.Entities;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(MessageContentEntity))]
[JsonSerializable(typeof(TextMessageContentEntity))]
[JsonSerializable(typeof(ImageMessageContentEntity))]
[JsonSerializable(typeof(string))]
public partial class MessageJsonContext : JsonSerializerContext;