using Newtonsoft.Json;

namespace RealtimeChat.Utils;

public static class ObjectExtensions
{
    public static string ToJson(this object @object, JsonSerializerSettings? settings = null)
    {
        return settings is not null 
            ? JsonConvert.SerializeObject(@object, settings)
            : JsonConvert.SerializeObject(@object);
    }
    
    public static T FromJson<T>(this string jsonString, JsonSerializerSettings? settings = null)
    {
        if (string.IsNullOrEmpty(jsonString)) throw new Exception("String can't be null or empty");
            
        return (settings is not null 
            ? JsonConvert.DeserializeObject<T>(jsonString, settings)
            : JsonConvert.DeserializeObject<T>(jsonString))!;
    }
}