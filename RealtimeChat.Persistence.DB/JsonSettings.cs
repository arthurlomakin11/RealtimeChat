using JsonSubTypes;
using Newtonsoft.Json;

namespace RealtimeChat.Persistence.DB;

public static class JsonSettings
{
    public static readonly JsonSerializerSettings MessageContentJsonSettings = new()
    {
        Converters =
        [
            JsonSubtypesConverterBuilder
                .Of<MessageContentEntity>("Type")
                .RegisterSubtype<TextMessageContentEntity>("text")
                .RegisterSubtype<ImageMessageContentEntity>("image")
                .SerializeDiscriminatorProperty()
                .Build()
        ]
    };
}