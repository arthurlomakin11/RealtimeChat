using EntityFramework.Exceptions.PostgreSQL;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RealtimeChat.Infrastructure.DB.Configurations;
using RealtimeChat.Persistence.DB.Entities;
using RealtimeChat.Persistence.DB.Interfaces;

namespace RealtimeChat.Infrastructure.DB;

public class RealtimeChatDbContext(DbContextOptions<RealtimeChatDbContext> dbContextOptions)
    : IdentityDbContext<ApplicationUser>(dbContextOptions), IRealtimeChatDbContext
{
    public DbSet<ChatRoomEntity> ChatRooms { get; set; } = null!;
    public DbSet<MessageEntity> Messages { get; set; } = null!;
    public DbSet<ChatRoomParticipantEntity> ChatRoomParticipants { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
        optionsBuilder.UseSnakeCaseNamingConvention();
        
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new DbFunctionsConfiguration());
        
        // Entities
        builder.ApplyConfiguration(new IdentityConfiguration());
        builder.ApplyConfiguration(new ChatRoomParticipantEntityConfiguration());
        builder.ApplyConfiguration(new ChatRoomEntityConfiguration());
        builder.ApplyConfiguration(new MessageEntityConfiguration());
    }
}