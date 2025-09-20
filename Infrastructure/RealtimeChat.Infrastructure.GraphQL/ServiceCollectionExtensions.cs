using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealtimeChat.Infrastructure.GraphQL;
using RealtimeChat.Persistence.GraphQL;

namespace RealtimeChat.API;

public static class ServiceCollectionExtensions
{
    public static void AddGraphQlServer<T>(this IServiceCollection services, IWebHostEnvironment environment)
        where T : DbContext
    {
        services
            .AddGraphQLServer()
            .InitializeOnStartup(keepWarm: true)
            .ModifyRequestOptions(opt => 
                opt.IncludeExceptionDetails = environment.IsDevelopment())
            .RegisterDbContextFactory<T>()
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