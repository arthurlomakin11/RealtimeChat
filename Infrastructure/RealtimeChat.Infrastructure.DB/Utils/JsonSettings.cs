using RealtimeChat.Persistence.DB.Entities;
using System.Text.Json;

namespace RealtimeChat.Persistence.DB;

public static class JsonSettings
{
    public static readonly JsonSerializerOptions MessageContentJsonSettings = new()
    {
        TypeInfoResolver = MessageJsonContext.Default,
        PropertyNameCaseInsensitive = true,
        AllowOutOfOrderMetadataProperties = true
    };
}