using Microsoft.EntityFrameworkCore;
using RealtimeChat.Infrastructure.DB;

namespace RealtimeChat.API;

public static class DbContextExtensions
{
    public static void AddDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
        builder.Services.AddDbContextFactory<RealtimeChatDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseNpgsql(
                connectionString,
                contextOptionsBuilder =>
                {
                    contextOptionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                    contextOptionsBuilder.MigrationsAssembly("RealtimeChat.Infrastructure.DB.Migrations");
                });
        });
    }
}