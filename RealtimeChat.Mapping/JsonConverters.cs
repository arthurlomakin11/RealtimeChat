using JsonSubTypes;
using Newtonsoft.Json;
using RealtimeChat.Domain;

namespace RealtimeChat.Mapping;

public static class JsonConverters
{
    public static JsonConverter GetJsonSubTypesConverter() =>
        JsonSubtypesConverterBuilder
            .Of<MessageContent>("Type")
            .RegisterSubtype<TextMessageContent>("Text")
            .RegisterSubtype<ImageMessageContent>("Image")
            .SerializeDiscriminatorProperty()
            .Build();
}