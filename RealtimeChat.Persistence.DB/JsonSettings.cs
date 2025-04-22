using System.Text.Json;
using RealtimeChat.Persistence.DB.Entities;

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