using RealtimeChat.Persistence.Repositories;

namespace RealtimeChat.API;

public static class InjectionExtensions
{
    public static void AddDependencyInjectionServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
    }
}