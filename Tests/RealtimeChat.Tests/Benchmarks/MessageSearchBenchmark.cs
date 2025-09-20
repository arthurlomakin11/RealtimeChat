using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using RealtimeChat.Infrastructure.DB.Context;
using RealtimeChat.Infrastructure.DB.Utils;
using RealtimeChat.Persistence.DB.Entities;
using RealtimeChat.Tests.DbContexts;
using Testcontainers.PostgreSql;

namespace RealtimeChat.Tests.Benchmarks;

[MemoryDiagnoser]
public class MessageSearchBenchmark
{
    private PostgreSqlContainer _containerWithIndexes = null!;
    private PostgreSqlContainer _containerWithoutIndexes = null!;
    
    private RealtimeChatDbContext _dbContextWithIndexes = null!;
    private RealtimeChatDbContext _dbContextWithoutIndexes = null!;

    [GlobalSetup]
    public async Task Setup()
    {
        var containerWithIndexesTask = InitializeDbContainer<RealtimeChatDbContext>(false);
        var containerWithoutIndexesTask = InitializeDbContainer<RealtimeChatDbContext>(true);
        
        var tasks = await Task.WhenAll(
            new List<Task<(PostgreSqlContainer container, RealtimeChatDbContext dbContext)>>
            {
                containerWithIndexesTask, containerWithoutIndexesTask
            });

        (_containerWithIndexes, _dbContextWithIndexes) = tasks[0];
        (_containerWithoutIndexes, _dbContextWithoutIndexes) = tasks[1];
    }

    private static async Task<(PostgreSqlContainer container, RealtimeChatDbContext dbContext)> 
        InitializeDbContainer<T>(bool noIndex)
        where T: RealtimeChatDbContext
    {
        var container = new PostgreSqlBuilder()
            .Build();
        
        await container.StartAsync();
        
        var dbContextOptions = new DbContextOptionsBuilder<T>()
            .UseNpgsql(container.GetConnectionString())
            .Options;
        
        var dbContext = new RealtimeChatDbContext(dbContextOptions);

        if (noIndex)
        {
            await dbContext.EnsureNoIndexAsync();
        }
        
        await dbContext.Database.EnsureCreatedAsync();
        await DbSeeder.Seed(dbContext, 10000);

        return (container, dbContext);
    }
    
    [GlobalCleanup]
    public async Task Cleanup()
    {
        await _containerWithIndexes.DisposeAsync();
        await _containerWithoutIndexes.DisposeAsync();
    }

    private static async Task Query(RealtimeChatDbContext dbContext)
    {
        const string searchString = "sample";
        const string contentName = nameof(MessageEntity.Content);
        
        await dbContext.Messages
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
    public Task QueryWithIndexes()
    {
	    return Query(_dbContextWithIndexes);
    }

    [Benchmark]
    public Task QueryWithoutIndexes()
    {
	    return Query(_dbContextWithoutIndexes);
    }
}