using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealtimeChat.Persistence.DB.Entities;

namespace RealtimeChat.Infrastructure.DB.Configurations;

public class ChatRoomEntityConfiguration : IEntityTypeConfiguration<ChatRoomEntity>
{
    public void Configure(EntityTypeBuilder<ChatRoomEntity> builder)
    {
        builder.HasKey(cr => cr.Id);

        builder.HasMany(cr => cr.Messages)
            .WithOne(m => m.ChatRoom)
            .HasForeignKey(m => m.ChatRoomId);
    }
}