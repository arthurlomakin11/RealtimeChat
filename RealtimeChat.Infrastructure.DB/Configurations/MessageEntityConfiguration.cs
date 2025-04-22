using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using RealtimeChat.Persistence.DB;
using RealtimeChat.Persistence.DB.Entities;
using RealtimeChat.Utils;

namespace RealtimeChat.Infrastructure.DB.Configurations;

public class MessageEntityConfiguration : IEntityTypeConfiguration<MessageEntity>
{
    private static readonly ValueConverter<MessageContentEntity, string> ContentConverter = new(
        v => v.ToJson(JsonSettings.MessageContentJsonSettings),
        v => v.FromJson<MessageContentEntity>(JsonSettings.MessageContentJsonSettings)
    );
    
    public void Configure(EntityTypeBuilder<MessageEntity> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId);
        
        builder.HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId);
        
        builder.Property(e => e.SentAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Property(e => e.Content)
            .HasColumnType("jsonb")
            .HasConversion(ContentConverter);
    }
}