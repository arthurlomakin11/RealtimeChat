using RealtimeChat.Domain.Models;

namespace RealtimeChat.Infrastructure.DB.Interface.Repositories;

public interface IChatRoomRepository
{
    IQueryable<ChatRoom> GetAllAsync();
    Task AddAsync(ChatRoom chatRoom);
}