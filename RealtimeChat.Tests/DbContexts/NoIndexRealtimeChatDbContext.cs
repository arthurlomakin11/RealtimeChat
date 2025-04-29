using Microsoft.EntityFrameworkCore;
using RealtimeChat.Infrastructure.DB;

namespace RealtimeChat.Tests.DbContexts;

public class NoIndexRealtimeChatDbContext(DbContextOptions options) : RealtimeChatDbContext(options)
{
    public static async Task EnsureNoIndexAsync(NoIndexRealtimeChatDbContext context)
    {
        await context.Database.ExecuteSqlRawAsync("DROP INDEX IF EXISTS idx_messages_content_text_trgm;");
    }
}