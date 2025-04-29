using RealtimeChat.Persistence.DB.Entities;

namespace RealtimeChat.Persistence.DB.Interfaces;

using Microsoft.EntityFrameworkCore;

public interface IRealtimeChatDbContext
{
    DbSet<ApplicationUser> Users { get; }
    DbSet<ChatRoomEntity> ChatRooms { get; }
    DbSet<MessageEntity> Messages { get; }
    DbSet<ChatRoomParticipantEntity> ChatRoomParticipants { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}