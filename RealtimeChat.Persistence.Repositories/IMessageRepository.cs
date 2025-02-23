namespace RealtimeChat.Persistence.Repositories;

public interface IMessageRepository
{
    Task<MessageContent> GetByIdAsync(int id);
    Task AddAsync(MessageContent message);
}