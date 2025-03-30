using RealtimeChat.Domain;

namespace RealtimeChat.Persistence.Repositories;

public interface IChatRoomRepository
{
    IQueryable<ChatRoom> GetAllAsync();
    Task AddAsync(ChatRoom chatRoom);
}