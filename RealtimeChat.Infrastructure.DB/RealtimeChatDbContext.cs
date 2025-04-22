using EntityFramework.Exceptions.PostgreSQL;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using RealtimeChat.Persistence.DB;
using RealtimeChat.Persistence.DB.Entities;
using RealtimeChat.Utils;

namespace RealtimeChat.Infrastructure.DB;

public class RealtimeChatDbContext(DbContextOptions<RealtimeChatDbContext> dbContextOptions)
    : IdentityDbContext<ApplicationUser>(dbContextOptions)
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
    
    private static readonly ValueConverter<MessageContentEntity, string> ContentConverter =
        new(
            v => v.ToJson(JsonSettings.MessageContentJsonSettings),
            v => v.FromJson<MessageContentEntity>(JsonSettings.MessageContentJsonSettings)
        );
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Identity
        builder.Entity<ApplicationUser>().ToTable("users");
        builder.Entity<IdentityRole>().ToTable("roles");
        builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");
        builder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");
        builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins");
        
        // ChatRoomParticipantEntity
        builder.Entity<ChatRoomParticipantEntity>()
            .HasKey(crp => crp.Id);

        builder.Entity<ChatRoomParticipantEntity>()
            .HasOne(crp => crp.ChatRoom)
            .WithMany(cr => cr.ChatRoomParticipants)
            .HasForeignKey(crp => crp.ChatRoomId);

        builder.Entity<ChatRoomParticipantEntity>()
            .HasOne(crp => crp.User)
            .WithMany(u => u.ChannelParticipants)
            .HasForeignKey(crp => crp.UserId);
        
        // ChatRoomEntity
        builder.Entity<ChatRoomEntity>()
            .HasKey(cr => cr.Id);

        builder.Entity<ChatRoomEntity>()
            .HasMany(cr => cr.Messages)
            .WithOne(m => m.ChatRoom)
            .HasForeignKey(m => m.ChatRoomId);
        
        // MessageEntity
        builder.Entity<MessageEntity>()
            .HasKey(m => m.Id);

        builder.Entity<MessageEntity>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId);
        
        builder.Entity<MessageEntity>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId);
        
        builder.Entity<MessageEntity>()
            .Property(e => e.SentAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Entity<MessageEntity>()
            .Property(e => e.Content)
            .HasColumnType("jsonb")
            .HasConversion(ContentConverter);
        
        builder.HasDbFunction(typeof(DatabaseFunctionsExtensions)
                .GetMethod(nameof(DatabaseFunctionsExtensions.JsonExtractPathText))!)
            .HasName("jsonb_extract_path_text")
            .IsBuiltIn();
    }
}