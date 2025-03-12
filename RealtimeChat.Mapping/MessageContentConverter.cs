using AutoMapper;
using Newtonsoft.Json;
using RealtimeChat.Domain;

namespace RealtimeChat.Mapping;

public class MessageContentConverter : ITypeConverter<string, MessageContent>
{
    public MessageContent Convert(string source, MessageContent destination, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source)) throw new ArgumentException("ContentJson is empty", nameof(source));

        var messageContent = JsonConvert.DeserializeObject<MessageContent>(source, 
            new JsonSerializerSettings
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