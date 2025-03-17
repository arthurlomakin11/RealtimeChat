using RealtimeChat.GraphQL;
using RealtimeChat.Infrastructure.DB;
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
            .AddType<TextMessageContentGraph>()
            .AddType<ImageMessageContentGraph>();
    }
    
    public static void UseGraphQlServer(this WebApplication app, string[] args)
    {
        app.MapGraphQL().RequireAuthorization();
        app.RunWithGraphQLCommands(args);
    }
}