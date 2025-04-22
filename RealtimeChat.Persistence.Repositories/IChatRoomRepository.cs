using RealtimeChat.Domain.Models;

namespace RealtimeChat.Persistence.Repositories;

public interface IChatRoomRepository
{
    IQueryable<ChatRoom> GetAllAsync();
    Task AddAsync(ChatRoom chatRoom);
}