using RealtimeChat.Infrastructure.DB;
using RealtimeChat.Infrastructure.GraphQL;
using RealtimeChat.Persistence.GraphQL;

namespace RealtimeChat.API;

public static class GraphQlExtensions
{
    public static void AddGraphQlServer(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddGraphQLServer()
            .ModifyRequestOptions(opt => 
                opt.IncludeExceptionDetails = builder.Environment.IsDevelopment())
            .RegisterDbContextFactory<RealtimeChatDbContext>()
            .AddQueryType<Query>()
            .AddMutationType<MessageMutation>()
            .AddSubscriptionType<MessageSubscription>()
            .AddType<TextMessageContentGraph>()
            .AddType<ImageMessageContentGraph>()
            .AddInMemorySubscriptions();
    }
    
    public static void UseGraphQlServer(this WebApplication app, string[] args)
    {
        app.UseWebSockets();
        app.MapGraphQL().RequireAuthorization();
        app.RunWithGraphQLCommands(args);
    }
}