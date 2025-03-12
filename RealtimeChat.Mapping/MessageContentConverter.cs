using AutoMapper;
using Newtonsoft.Json;
using RealtimeChat.Domain;
using RealtimeChat.Utils;

namespace RealtimeChat.Mapping;

public class MessageContentConverter : ITypeConverter<string, MessageContent>
{
    public MessageContent Convert(string source, MessageContent destination, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source)) throw new ArgumentException("ContentJson is empty", nameof(source));

        var messageContent = source.FromJson<MessageContent>(new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Converters = new List<JsonConverter>
            {
                JsonConverters.GetJsonSubTypesConverter()
            }
        });
            
        if (messageContent == null) throw new Exception("Failed to deserialize MessageContent");

        return messageContent;
    }
}