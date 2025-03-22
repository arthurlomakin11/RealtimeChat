namespace RealtimeChat.Persistence.Repositories;

public interface IMessageRepository
{
    Task<Message> GetByIdAsync(int id);
    Task<Message> AddAsync(Message message);
    Task<Message> UpdateContentAsync(int messageId, MessageContent message);
    Task<Message> DeleteAsync(int messageId);
}