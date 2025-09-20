using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using RealtimeChat.Infrastructure.DB.Context;
using RealtimeChat.Infrastructure.DB.Interface.Repositories;
using RealtimeChat.Infrastructure.DB.Repositories;

namespace RealtimeChat.API;

public static class ServiceCollectionExtensions
{
    public static void AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        // Repositories
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        
        // Context
        var connectionString = configuration
	        .GetConnectionString("DefaultConnectionString");
        
        services.AddDbContextFactory<RealtimeChatDbContext>(optionsBuilder =>
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.UseJsonNet();
            
            optionsBuilder.UseNpgsql(dataSourceBuilder.Build(),
                contextOptionsBuilder =>
                {
                    contextOptionsBuilder
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });
                
            if (!environment.IsDevelopment())
            {
                return;
            }

            var loggerFactory = LoggerFactory
                .Create(loggingBuilder => 
                    loggingBuilder.AddConsole());
            
            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
    }
}