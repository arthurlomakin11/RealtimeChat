using Microsoft.EntityFrameworkCore;

using Npgsql;

using RealtimeChat.Infrastructure.DB;
using RealtimeChat.Persistence.DB.Interfaces;

namespace RealtimeChat.API;

public static class DbContextExtensions
{
    public static void AddDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
        builder.Services.AddDbContextFactory<RealtimeChatDbContext>(optionsBuilder =>
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.UseJsonNet();
            
            optionsBuilder.UseNpgsql(dataSourceBuilder.Build(),
                contextOptionsBuilder =>
                {
                    contextOptionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                    contextOptionsBuilder.MigrationsAssembly("RealtimeChat.Infrastructure.DB.Migrations");
                });
                
            if (!builder.Environment.IsDevelopment()) return;
            
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(loggingBuilder => loggingBuilder.AddConsole()))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
        
        builder.Services.AddScoped<IRealtimeChatDbContext, RealtimeChatDbContext>();
        builder.Services.AddHostedService<EfCoreWarmupService>();
    }
}