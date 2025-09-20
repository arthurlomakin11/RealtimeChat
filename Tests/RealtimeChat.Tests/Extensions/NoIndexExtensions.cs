using Microsoft.EntityFrameworkCore;
using RealtimeChat.Infrastructure.DB.Context;

namespace RealtimeChat.Tests.DbContexts;

public static class NoIndexExtensions
{
    public static async Task EnsureNoIndexAsync(this RealtimeChatDbContext context)
    {
        await context.Database.ExecuteSqlRawAsync("DROP INDEX IF EXISTS idx_messages_content_text_trgm;");
    }
}