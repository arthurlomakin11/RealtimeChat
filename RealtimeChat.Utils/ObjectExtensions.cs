using System.Text.Json;

namespace RealtimeChat.Utils;

public static class ObjectExtensions
{
    public static string ToJson(this object @object) => 
        JsonSerializer.Serialize(@object);
    public static string ToJson(this object @object, JsonSerializerOptions options) => 
        JsonSerializer.Serialize(@object, options);

    public static T FromJson<T>(this string jsonString, JsonSerializerOptions? options = null)
    {
        if (string.IsNullOrEmpty(jsonString))
        {
            throw new Exception("String can't be null or empty");
        }
            
        return (options is not null 
            ? JsonSerializer.Deserialize<T>(jsonString, options)
            : JsonSerializer.Deserialize<T>(jsonString))!;
    }
}