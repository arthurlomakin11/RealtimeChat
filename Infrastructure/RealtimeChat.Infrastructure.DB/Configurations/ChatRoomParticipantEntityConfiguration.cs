using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealtimeChat.Persistence.DB.Entities;

namespace RealtimeChat.Infrastructure.DB.Configurations;

public class ChatRoomParticipantEntityConfiguration : IEntityTypeConfiguration<ChatRoomParticipantEntity>
{
    public void Configure(EntityTypeBuilder<ChatRoomParticipantEntity> builder)
    {
        builder.HasKey(crp => crp.Id);

        builder.HasOne(crp => crp.ChatRoom)
            .WithMany(cr => cr.ChatRoomParticipants)
            .HasForeignKey(crp => crp.ChatRoomId);

        builder.HasOne(crp => crp.User)
            .WithMany(u => u.ChannelParticipants)
            .HasForeignKey(crp => crp.UserId);
    }
}