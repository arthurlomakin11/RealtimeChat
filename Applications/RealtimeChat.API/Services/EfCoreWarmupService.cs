using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealtimeChat.Domain.Models;
using RealtimeChat.Infrastructure.DB.Context;

namespace RealtimeChat.API;

public sealed class EfCoreWarmupService(IServiceProvider serviceProvider) 
    : IHostedService, IDisposable
{
    private Timer? _timer;
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(30);

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(ExecuteDbQuery, null, TimeSpan.FromSeconds(0), Interval);
        return Task.CompletedTask;
    }
    
    private async void ExecuteDbQuery(object? state)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<RealtimeChatDbContext>();

        _ = db.Model;
        var messageEntity = await db.Messages.FirstOrDefaultAsync();

        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        mapper.Map<Message>(messageEntity);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}