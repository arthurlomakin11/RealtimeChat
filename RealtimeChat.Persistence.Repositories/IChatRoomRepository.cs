namespace RealtimeChat.Persistence.Repositories;

public interface IChatRoomRepository
{
    Task<ChatRoom> GetByIdAsync(int id);
    Task AddAsync(ChatRoom chatRoom);
}