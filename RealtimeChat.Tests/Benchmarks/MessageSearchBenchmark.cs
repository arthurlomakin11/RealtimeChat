using BenchmarkDotNet.Attributes;

using Microsoft.EntityFrameworkCore;

using RealtimeChat.Infrastructure.DB;
using RealtimeChat.Persistence.DB;
using RealtimeChat.Persistence.DB.Entities;
using RealtimeChat.Persistence.DB.Interfaces;
using RealtimeChat.Tests.DbContexts;

namespace RealtimeChat.Tests.Benchmarks;

[MemoryDiagnoser]
public class MessageSearchBenchmark
{
    private RealtimeChatDbContext _dbContextWithIndexes = null!;
    private NoIndexRealtimeChatDbContext _dbContextWithoutIndexes = null!;

    private static string GetConnectionString(bool withIndexes)
    {
        var postfix = withIndexes ? "with_indexes" : "without_indexes";
        return
            $"Server=localhost;Port=5432;Username=postgres;Password=postgres;Database=test_realtime_chat_db_{postfix};" +
            $"Include Error Detail=true;MaxPoolSize=10;MinPoolSize=2;";
    }

    [GlobalSetup]
    public async Task Setup()
    {
        // DbContextOptionsBuilder
        var withOptions = new DbContextOptionsBuilder<RealtimeChatDbContext>()
            .UseNpgsql(GetConnectionString(true))
            .Options;

        var noOptions = new DbContextOptionsBuilder<NoIndexRealtimeChatDbContext>()
            .UseNpgsql(GetConnectionString(false))
            .Options;

        // Properties init
        _dbContextWithIndexes = new RealtimeChatDbContext(withOptions);
        _dbContextWithoutIndexes = new NoIndexRealtimeChatDbContext(noOptions);

        // Setup
        await _dbContextWithIndexes.Database.EnsureCreatedAsync();
        await _dbContextWithoutIndexes.Database.EnsureCreatedAsync();
        await NoIndexRealtimeChatDbContext.EnsureNoIndexAsync(_dbContextWithoutIndexes);

        // Seed with data
        await DbSeeder.Seed(_dbContextWithIndexes, 10000);
        await DbSeeder.Seed(_dbContextWithoutIndexes, 10000);
    }

    private async Task Query(IRealtimeChatDbContext dbContext)
    {
        const string searchString = "sample";
        const string contentName = nameof(MessageEntity.Content);
        
        var result = await dbContext.Messages
            .Where(m =>
                DatabaseFunctionsExtensions.JsonExtractPathText(
                    EF.Property<string>(m, contentName), "Type") == "text" &&
                EF.Functions.ILike(
                    DatabaseFunctionsExtensions.JsonExtractPathText(
                        EF.Property<string>(m, contentName), "Text"),
                    $"%{searchString}%"))
            .ToListAsync();
    }

    [Benchmark]
    public Task QueryWithIndexes() => Query(_dbContextWithIndexes);
    
    [Benchmark]
    public Task QueryWithoutIndexes() => Query(_dbContextWithoutIndexes);
}